<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="roleName">
          <el-input v-model="list.model.roleName" clearable />
        </el-form-item>
        <el-form-item label="编码：" prop="roleCode">
          <el-input v-model="list.model.roleCode" clearable />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.edit" @click="edit(row)" :disabled="row.isSpecified" />
        <em-button v-bind="buttons.bindMenu" @click="bindMenu(row)" />
        <em-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="refresh" :disabled="row.isSpecified" />
      </template>
    </em-list>

    <!--保存页-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
    <!--绑定菜单-->
    <bind-menu-page :id="curr.id" :visible.sync="dialog.bindMenu" />
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../_components/save'
import BindMenuPage from '../_components/menu-bind'

// 接口
const api = $api.admin.role

export default {
  mixins: [mixins.list],
  name: page.name,
  components: { SavePage, BindMenuPage },
  data() {
    return {
      api,
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          //角色编码
          roleCode: '',
          //角色名称
          roleName: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      dialog: {
        bindMenu: false
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    bindMenu(row) {
      this.curr.id = row.id
      this.dialog.bindMenu = true
    }
  }
}
</script>
