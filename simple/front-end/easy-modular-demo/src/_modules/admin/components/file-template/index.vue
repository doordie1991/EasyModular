<template>
  <div class="file-template" @click="download">{{ templateName }}<em-icon name="import" /></div>
</template>

<script>
const { queryByCode } = $api.admin.importTemplate
export default {
  data() {
    return {
      //附件
      attachmentId: '',
      //模板名称
      templateName: ''
    }
  },
  props: {
    //模块编码
    moduleCode: {
      type: String,
      required: true
    },
    //模板编码
    templateCode: {
      type: String,
      required: true
    }
  },
  methods: {
    /**
     * @description:获取导入模板数据
     * @param {*}
     */
    async getData() {
      const params = { moduleCode: this.moduleCode, templateCode: this.templateCode }
      const data = await queryByCode(params)
      this.attachmentId = data.attachmentId
      this.templateName = data.templateName
    },

    /**
     * @description: 下载导入模板
     * @param {*}
     */
    download() {
      $api.admin.attachment.download(this.attachmentId)
    }
  },
  created() {
    this.getData()
  }
}
</script>

<style lang="scss" scoped>
.file-template {
  color: #409eff;
  cursor: pointer;
  text-decoration: underline;

  .em-icon {
    margin-left: 5px;
    font-size: 16px;
  }
}
</style>
