<template>
  <div :class="['em-attachment-upload-img', disabled ? 'disabled' : '']" :style="style" v-loading="loading">
    <div v-if="disabled" class="disabled-bg"></div>
    <el-upload v-bind="uploadOptions">
      <em-attachment-img v-show="imgShow" :id="id" />
      <div v-show="!imgShow" class="text-box">
        <i class="el-icon-upload" :style="{ fontSize: iconSize }" v-if="showIcon"></i>
        <div class="upload__text"><em>{{btnTxt}}</em></div>
        <div class="tip">
          <slot name="tip">{{ tip }}</slot>
        </div>
      </div>
    </el-upload>
    <span v-show="imgShow" class="remove" @click="reset">
      <em-icon name="close" />
    </span>
  </div>
</template>
<script>
import mixins from '../upload-mixins'

// 接口
const api = $api.admin.attachment

export default {
  mixins: [mixins],
  props: {
    //宽度
    width: {
      type: String,
      default: '100px'
    },
    //高度
    height: {
      type: String,
      default: '100px'
    },
    //底部提示语
    tip: String,
    //接受上传的文件类型
    accept: {
      type: String,
      default: 'image/jpeg,image/png'
    },
    //可接受文件类型
    fileType: {
      type: Array,
      default() {
        return ['jpg', 'jpeg', 'png']
      }
    },
    //图标大小
    iconSize: {
      type: String,
      default: '5em'
    },
    //展示图标
    showIcon: {
      type: Boolean,
      default: true
    },
    //上传按钮文字
    btnTxt: {
      type: String,
      default: '点击上传'
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
        accept: this.accept,
        drag: this.drag,
        autoUpload: true,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        onError: this.onError,
        disabled: this.disabled
      }
    },
    imgShow() {
      return this.id && this.id != this.$emptyGuid
    },
    style() {
      return { width: this.width, height: this.height }
    }
  },
  methods: {
    /**
     * @description: 上传前事件
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
     * @description: 上传成功事件
     * @param {*} res
     * @param {*} file
     */
    onSuccess(res, file) {
      const data = res.data.data
      if (res.code === '0') {
        this.id = data.id
        this.$emit('input', this.id)
        this.$emit('success', res, file)
      } else {
        this.$emit('error')
      }
      this.loading = false
    },

    /**
     * @description: 重置
     * @param {*}
     */
    reset() {
      this.id = ''
      this.$emit('input', this.id)
    }
  }
}
</script>

<style lang="scss">
.em-attachment-upload-img {
  position: relative;
  display: inline-block;
  box-sizing: border-box;
  border: 1px dashed #ccc;
  background-color: #fff;
  text-align: center;
  color: #8c939d;
  cursor: pointer;
  border-radius: 5px;
  overflow: hidden;

  > div {
    width: 100%;
    height: 100%;
  }

  .el-upload {
    width: 100%;
    height: 100%;
  }

  .el-upload-dragger {
    position: relative;
    top: 50%;
    border: none;
    margin-top: -50%;
    width: 100%;
    height: auto;
    .text-box {
      font-size: 14px;

      .el-icon-upload {
        margin: auto;
        font-size: 5em;
        color: #c0c4cc !important;
      }

      em {
        color: #409eff;
        font-style: normal;
      }

      .text {
        font-size: 13px;
        line-height: 130%;
      }
    }
  }

  .el-loading-spinner {
    line-height: 100%;
  }

  .remove {
    position: absolute;
    display: none;
    top: 0;
    right: 0;
    width: 30px;
    height: 30px;
    line-height: 27px;
    border-radius: 15px;
    background-color: #f56c6c;
    color: #ccc;
    font-size: 16px;

    &:hover {
      color: #fff;
    }
  }

  &:hover {
    border-color: #409eff;

    .remove {
      display: inline-block;
    }
  }

  &.disabled:hover {
    border: 1px dashed #ccc;

    .remove {
      display: none;
    }
  }

  .disabled-bg {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: #f5f7fa;
    z-index: 999;
    opacity: 0.5;
    cursor: not-allowed;
  }
}
</style>
