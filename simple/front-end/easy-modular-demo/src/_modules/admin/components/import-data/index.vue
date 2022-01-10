<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_">
    <el-form
      ref="form"
      :model="form"
      :rules="rules"
      label-width="100px"
      label-position="left"
      v-loading="loading"
      element-loading-text="拼命导入中..."
      :element-loading-background="loadingBackground"
      :element-loading-spinner="loadingSpinner"
    >
      <el-form-item label="导入文件" prop="fileName">
        <div class="upload-container">
          <div class="input">
            <el-input :value="form.fileName" readonly />
          </div>
          <div class="button">
            <el-upload ref="upload" v-bind="uploadOptions">
              <em-button type="primary" text="上传" icon="upload" @click="onClick" />
            </el-upload>
          </div>
        </div>
      </el-form-item>
      <el-form-item label="下载模板" v-if="templateCode && templateCode.length > 0">
        <em-file-template :moduleCode="moduleCode" :templateCode="templateCode"></em-file-template>
      </el-form-item>
    </el-form>

    <template v-slot:footer>
      <em-button type="success" size="small" @click="confirm">确定</em-button>
      <em-button type="info" size="small" @click="visible_ = false">关闭</em-button>
    </template>
  </em-dialog>
</template>
<script>
import { mapState } from 'vuex'
import { mixins } from 'easy-modular-ui'
const api = $api.admin.importTemplate
export default {
  mixins: [mixins.visible],
  data() {
    return {
      loading: false,
      form: {
        fileName: ''
      },

      dialog: {
        title: '导入',
        icon: 'cloud-upload',
        width: '600px',
        height: '300px',
        noScrollbar: true,
        footer: true,
        padding: 16
      },
      rules: {
        fileName: [{ required: true, message: '请上传文件' }]
      }
    }
  },
  computed: {
    ...mapState('app/token', ['accessToken']),
    ...mapState('app/loading', { loadingBackground: 'background', loadingSpinner: 'spinner' }),
    uploadOptions() {
      return {
        action: this.importApiUrl,
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: this.extra,
        drag: false,
        autoUpload: false,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onChange: this.onChange,
        onSuccess: this.onSuccess,
        onError: this.onError
      }
    },
    maxSizeBytes() {
      if (this.maxSize) {
        const max = this.maxSize.toLowerCase()
        if (max.endsWith('kb')) {
          return parseFloat(max.replace('kb', '')) * 1024
        } else if (max.endsWith('mb')) {
          return parseFloat(max.replace('mb', '')) * 1024 * 1024
        } else if (max.endsWith('gb')) {
          return parseFloat(max.replace('gb', '')) * 1024 * 1024 * 1024
        } else if (max.endsWith('tb')) {
          return parseFloat(max.replace('tb', '')) * 1024 * 1024 * 1024 * 1024
        } else if (max.endsWith('b')) {
          return parseFloat(max.replace('b', ''))
        } else {
          return parseFloat(max)
        }
      }
      return ''
    }
  },
  props: {
    //导入模块编码
    moduleCode: {
      type: String
    },
    //导入模板编码
    templateCode: {
      type: String
    },
    //导入接口地址
    importApiUrl: {
      type: String,
      require: true
    },
    //额外数据
    extra: Object,
    //可接受文件类型
    fileType: {
      type: Array,
      default: () => {
        return ['xls', 'xlsx']
      }
    },
    //文件最大大小
    maxSize: String,
    //接受上传的文件类型
    accept: String
  },
  methods: {
    /**
     * @description:点击上传
     * @param {*}
     */
    onClick() {
      if (this.loading) {
        return
      }
      this.$refs.upload.clearFiles()
    },

    /**
     * @description: 确认
     * @param {*}
     */

    confirm() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.loading = true
          this.$refs.upload.submit()
        }
      })
    },

    /**
     * @description: 文件状态改变时
     * @param {*}
     */
    onChange(file) {
      this.form.fileName = file.name
    },

    /**
     * @description: 上传前
     * @param {*} file
     */
    onBeforeUpload(file) {
      // 文件格式验证
      if (!this.verifyFileType(file)) {
        return false
      }
      // 验证是否超出最大上传数
      if (this.maxSizeBytes && file.size > this.maxSizeBytes) {
        this._error(`文件大小${this.getSizeUnit(file.size)}超过了最大允许值${this.maxSize}`)
        return false
      }
      if (this.beforeUpload && typeof this.beforeUpload === 'function' && !this.beforeUpload(file)) {
        return false
      }

      this.loading = true
      return true
    },

    /**
     * @description: 上传失败
     * @param {*}
     */
    onError() {
      this._error('上传失败')
      this.$emit('error')
      this.loading = false
    },

    /**
     * @description: 上传成功
     * @param {*} res
     * @param {*} file
     */
    onSuccess(res, file) {
      this.loading = false
      if (res.code != '0') {
        this._error(res.msg)
        return
      }
      this._success('导入成功')
      this.$emit('success')
      this.hide()
    },

    /**
     *
     * @description: 获取文件后缀名
     * @param {*}
     */
    getFileExt(name) {
      let ext = ''
      const arr = name.split('.')
      if (arr.length > 1) {
        ext = arr[arr.length - 1]
      }
      return ext
    },

    /**
     * @description: 验证文件类型
     * @param {*} file
     */
    verifyFileType(file) {
      if (this.fileType && this.fileType.length > 0) {
        var ext = this.getFileExt(file.name)
        if (this.fileType.every(m => m.toLowerCase() !== ext)) {
          this._error(`.${ext} 文件类型不支持！`)
          return false
        }
      }
      return true
    }
  }
}
</script>
<style lang="scss" scoped>
.upload-container {
  position: relative;
  padding-right: 80px;
  width: 100%;
  box-sizing: border-box;
  .input {
    .el-input__inner {
      border-top-right-radius: 0;
      border-bottom-right-radius: 0;
    }
  }
  .button {
    position: absolute;
    right: 0;
    top: 0;
    width: 80px;
    height: 100%;

    > div,
    .el-upload,
    .em-button {
      height: 100%;
    }

    .em-button {
      border-top-left-radius: 0;
      border-bottom-left-radius: 0;
    }
  }
}
</style>
