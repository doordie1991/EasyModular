<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="权限编码" prop="permissionCode">
          <el-input v-model="form.model.permissionCode" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="权限名称" prop="permissionName">
          <el-input v-model="form.model.permissionName" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="区域" prop="area">
          <el-input v-model="form.model.area" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="控制器" prop="controller">
          <el-input v-model="form.model.controller" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="方法" prop="action">
          <el-input v-model="form.model.action" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="请求方式" prop="httpMethod">
          <el-radio v-model="form.model.httpMethod" label="GET">GET</el-radio>
          <el-radio v-model="form.model.httpMethod" label="POST">POST</el-radio>
          <el-radio v-model="form.model.httpMethod" label="DELETE">DELETE</el-radio>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="数据来源" prop="source">
          <el-radio v-model="form.model.source" :label="0">手动</el-radio>
          <el-radio v-model="form.model.source" :label="1">自动</el-radio>
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.permission

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '权限',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '600px',
        model: {
          permissionCode: '',
          permissionName: '',
          area: '',
          controller: '',
          action: '',
          httpMethod: '',
          source: 0
        },
        rules: {
          permissionCode: [{ required: true, message: '请输入权限编码' }],
          permissionName: [{ required: true, message: '请输入权限名称' }],
          area: [{ required: true, message: '请输入区域' }],
          controller: [{ required: true, message: '请输入控制器' }],
          action: [{ required: true, message: '请输入方法' }],
          httpMethod: [{ required: true, message: '请选择请求方式' }],
          source: [{ required: true, message: '请选择数据来源' }]
        }
      }
    }
  }
}
</script>
