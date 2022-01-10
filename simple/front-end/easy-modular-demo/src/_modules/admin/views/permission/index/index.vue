<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="权限名称：" prop="permissionName">
          <el-input v-model="list.model.permissionName" clearable />
        </el-form-item>
        <el-form-item label="权限编码：" prop="permissionCode">
          <el-input v-model="list.model.permissionCode" clearable />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
        <em-button v-bind="buttons.sync" @click="sync()" />
      </template>

      <!--请求方式-->
      <template v-slot:col-httpMethod="{ row }">
        <el-tag v-if="row.httpMethod === 'GET'" type="success">GET</el-tag>
        <el-tag v-if="row.httpMethod === 'POST'" type="primary">POST</el-tag>
        <el-tag v-if="row.httpMethod === 'DELETE'" type="danger">DELETE</el-tag>
      </template>

      <!--数据来源-->
      <template v-slot:col-source="{ row }">
        <el-tag v-if="row.source === 0" type="success">手动</el-tag>
        <el-tag v-if="row.source === 1" type="primary">自动</el-tag>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.edit" @click="edit(row)" />
        <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </em-list>

    <!--编辑-->
    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../_components/save'

// 接口
const api = $api.admin.permission

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '320',
        model: {
          permissionCode: '',
          permissionName: '',
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    async sync() {
      await this._confirm('您确定要同步权限数据吗?')
      await api.sync()
      await this._success('同步成功')
      this.refresh()
    }
  }
}
</script>
