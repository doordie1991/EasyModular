<template>
  <div class="em-attachment-upload">
    <div class="upload-container">
      <div class="input">
        <el-input :value="fileName" readonly :disabled="disabled" />
      </div>
      <div class="button">
        <el-upload ref="upload" v-bind="uploadOptions">
          <em-button :type="btnType" text="上传" :icon="icon_" @click="onClick" :loading="loading" />
        </el-upload>
      </div>
    </div>
    <div class="fileList">
      <el-table
        :data="fileList"
        size="mini"
        style="width: 10
      0%"
        v-if="fileList.length > 0"
      >
        <el-table-column align="center" type="index" width="50"></el-table-column>
        <el-table-column align="center" prop="fileName" label="名称" min-width="160"></el-table-column>
        <el-table-column align="center" prop="sizeCn" label="大小" width="80"></el-table-column>
        <el-table-column align="center" prop="ext" label="扩展" width="80"></el-table-column>
        <el-table-column align="center" label="操作" width="160">
          <template v-slot:default="{ row }">
            <em-button type="text" icon="download" text="下载" @click="onDownload(row)" />
            <em-button type="text" icon="delete" text="删除" @click="onRemove(row)" />
          </template>
        </el-table-column>
      </el-table>
    </div>
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
      fileName: '',
      fileList: []
    }
  },
  props: {
    //按钮类型
    btnType: {
      type: String,
      default: 'primary'
    },
    //不显示按钮图标
    noIcon: Boolean
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
          name: this.name
        },
        accept: this.accept,
        drag: false,
        autoUpload: true,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        onError: this.onError,
        disabled: this.disabled
      }
    },
    icon_() {
      return this.loading ? 'loading' : this.noIcon ? '' : 'upload'
    }
  },
  methods: {
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
     * @description: 上传成功
     * @param {*} res
     * @param {*} file
     */
    onSuccess(res, file) {
      const data = res.data.data
      if (res.code === '0') {
        if (this.multiple) {
          this.fileList.push(data)
          this.id = this.id ? `${this.id},${data.id}` : data.id
        } else {
          this.id = data.id
          this.fileName = file.name
        }

        this.$emit('input', this.id)
        this.$emit('success', res, file)
      } else {
        this.$emit('error')
      }
      this.loading = false
    },

    /**
     * @description: 下载
     * @param {*} row
     */
    onDownload(row) {
      api.download(row.id)
    },

    /**
     * @description: 移除
     * @param {*} row
     */
    onRemove(row) {
      this._confirm('您确认要删除该数据吗').then(() => {
        this.fileList.splice(this.fileList.indexOf(row), 1)
      })
    },

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
     * @description: 获取附件数据
     * @param {*}
     */
    async getData() {
      if (this.id && this.id != this.$emptyGuid) {
        const ids = this.id.split(',')
        let res = await api.queryByIds(ids)
        this.fileList = res
        if (this.fileList.length > 0 && !this.multiple) {
          this.fileName = this.fileList[0].fileName
        }
      } else {
        this.fileList = []
        this.fileName = ''
      }
    },

    /**
     * @description: 重置
     * @param {*}
     */
    reset() {
      this.id = this.$emptyGuid
      this.fileName = ''
      this.fileList = []
      this.$emit('input', this.id)
    }
  },
  watch: {
    name(val) {
      this.fileName = val
    },
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

<style lang="scss">
.fileList {
  width: 100%;
}
.em-attachment-upload {
  width: 100%;
}
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
