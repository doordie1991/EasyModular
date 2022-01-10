<template>
  <div class="attachment-upload-img-multi">
    <el-upload v-bind="uploadOptions" list-type="picture-card" :on-preview="handlePictureCardPreview" :on-remove="handleRemove" :disabled="disabled" :class="{ disUoload: disabled | disUoload }">
      <i class="el-icon-plus"></i>
    </el-upload>
    <el-dialog :visible.sync="dialogVisible" size="tiny">
      <img width="100%" :src="dialogImageUrl" alt />
    </el-dialog>
  </div>
</template>
<script>
import mixins from '../upload-mixins'

// 接口
const api = $api.admin.attachment

export default {
  mixins: [mixins],
  data() {
    return {
      //图片预览窗口显示
      dialogVisible: false,
      //图片预览路径
      dialogImageUrl: '',
      //文件路径
      fileList: [],
      //文件Id
      fileIds: [],
      //禁止上传
      disUoload: false
    }
  },
  props: {
    //最大允许上传个数
    limit: {
      type: Number,
      default: 5
    },
    //是否禁用
    disabled: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    uploadOptions() {
      return {
        action: api.getUploadUrl(),
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: {
          module: this.module,
          group: this.group,
          name: this.name,
          auth: this.auth
        },
        fileList: this.fileList,
        accept: this.accept,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        disabled: this.disabled,
        limit: this.limit
      }
    }
  },
  methods: {
    /**
     * @description: 文件上传前回调
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
     * @description: 文件上传成功回调
     * @param {*} res
     * @param {*} file
     */
    onSuccess(res, file) {
      const data = res.data.data
      if (res.code === '0') {
        file.id = data.id
        if (this.fileIds.indexOf(file.id) < 0) this.fileIds.push(file.id)
        this.disUoload = this.fileIds.length === this.limit
        this.id = this.fileIds.join('|')
        this.$emit('input', this.id)
        this.$emit('success', res, file)
      } else {
        this.$emit('error')
      }
    },

    /**
     * @description:移除图片
     * @param {*} file
     */
    handleRemove(file) {
      this.fileIds.splice(this.fileIds.indexOf(file.id), 1)
      this.id = this.fileIds.join('|')
      this.$emit('input', this.id)
    },

    /**
     * @description:图片预览
     * @param {*} file
     */
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url
      this.dialogVisible = true
    },

    /**
     * @description: 加载图片
     * @param {*}
     */
    async getData() {
      this.fileList = []
      this.fileIds = []
      if (!this.id) return
      const ar_id = this.id.split('|')
      this.disUoload = ar_id.length === this.limit
      ar_id.forEach(v => {
        api.preview(v).then(res => {
          const img = { id: v, url: res }
          this.fileList.push(img)
          if (this.fileIds.indexOf(v) < 0) this.fileIds.push(v)
        })
      })
    }
  },
  watch: {
    value: {
      handler(newVal) {
        this.id = newVal
        this.getData()
      },
      immediate: true
    }
  }
}
</script>

<style lang="scss" scoped>
/deep/ .el-upload--picture-card {
  width: 100px;
  height: 100px;
  line-height: 100px;
}
/deep/ .el-upload-list--picture-card .el-upload-list__item {
  width: 100px;
  height: 100px;
}
/deep/ .disUoload .el-upload--picture-card {
  display: none; /* 上传按钮隐藏 */
}
</style>
