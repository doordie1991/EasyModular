<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="module">
          <em-module-select v-model="list.model.module"></em-module-select>
        </el-form-item>
        <el-form-item label="资源编码：" prop="code">
          <el-input v-model="list.model.code" clearable placeholder="请输入资源编码" :size="fontSize" />
        </el-form-item>
        <el-form-item label="资源名称：" prop="name">
          <el-input v-model="list.model.name" clearable placeholder="请输入资源名称" :size="fontSize" />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
        <em-button v-bind="buttons.sync" @click="sync()" />
      </template>

      <!--数据来源-->
      <template v-slot:col-source="{ row }">
        <el-tag v-if="row.source === 0" type="success">手动</el-tag>
        <el-tag v-if="row.source === 1" type="danger">自动</el-tag>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.edit" @click="edit(row)" />
        <em-button v-bind="buttons.permissions" @click="permissions(row)" />
        <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </em-list>

    <!--编辑-->
    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
    <!--权限设置-->
    <permission-page :id="curr.id" :title="permissionTitle" :visible.sync="permissionShow" />
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../_components/save'
import PermissionPage from '../_components/permission'

// 接口
const api = $api.admin.resource

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage, PermissionPage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '250',
        model: {
          //模块
          module: '',
          //资源编码
          code: '',
          //资源名称
          name: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      permissionShow: false,
      permissionTitle: ''
    }
  },

  methods: {
    /**
     * @description: 同步
     * @param {*}
     */
    async sync() {
      await this._confirm('您确定要同步权限数据吗?')
      await api.sync()
      await this._success('同步成功')
      this.refresh()
    },

    /**
     * @description: 权限设置
     * @param {*}
     */
    permissions(row) {
      this.curr.id = row.id
      this.permissionTitle = row.name
      this.permissionShow = true
    }
  }
}
</script>
