<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="模块" prop="moduleCode">
          <em-module-select v-model="form.model.moduleCode"></em-module-select>
        </el-form-item>
        <el-form-item label="模板编码" prop="templateCode">
          <el-input v-model="form.model.templateCode" :size="fontSize" />
        </el-form-item>
        <el-form-item label="模板名称" prop="templateName">
          <el-input v-model="form.model.templateName" :size="fontSize" />
        </el-form-item>
        <el-form-item label="附件" prop="attachmentId">
          <em-attachment-upload v-model="form.model.attachmentId" module="admin" group="importTemplate"></em-attachment-upload>
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.importTemplate

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '导入模板',
      actions: { add, edit, update },
      form: {
        width: '800px',
        height: '550px',
        labelWidth: '80px',
        labelPosition: 'left',
        model: {
          //模块编码
          moduleCode: '',
          //模板编码
          templateCode: '',
          //模板名称
          templateName: '',
          //附件
          attachmentId: ''
        },
        rules: {
          moduleCode: [{ required: true, message: '请选择模块' }],
          templateCode: [{ required: true, message: '请输入模板编码' }],
          templateName: [{ required: true, message: '请输入模板名称' }],
          attachmentId: [{ required: true, message: '请上传附件' }]
        }
      }
    }
  }
}
</script>
