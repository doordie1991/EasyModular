<template>
  <em-container>
    <em-split v-model="split">
      <template v-slot:left>
        <role-tree ref="tree" @change="selectRole" />
      </template>
      <template v-slot:right>
        <em-list ref="list" v-bind="list">
          <!--查询条件-->
          <template v-slot:querybar>
            <el-form-item label="用户名称：" prop="userName">
              <el-input v-model="list.model.userName" clearable />
            </el-form-item>
            <el-form-item label="用户账号：" prop="userCode">
              <el-input v-model="list.model.userCode" clearable />
            </el-form-item>
          </template>

          <!--操作栏-->
          <template v-slot:operatebar="{ total }">
            <em-button v-bind="buttons.add" @click="add(total)" />
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{ row }">
            <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
          </template>
        </em-list>
      </template>
    </em-split>

    <!--用户选择-->
    <em-user-select-dialog :visible.sync="dialog.save" @change="saveUser"></em-user-select-dialog>
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import RoleTree from '../_components/tree'

// 接口
const api = $api.admin.roleUser

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { RoleTree },
  data() {
    return {
      split: 0.2,
      list: {
        queryOnCreated: false,
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '200',
        model: {
          userCode: '',
          userName: '',
          roleId: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    //选择角色
    selectRole(data) {
      this.list.model.roleId = data.id
      this.refresh()
    },
    //添加
    add() {
      const roleId = this.list.model.roleId
      if (!roleId) {
        this._warning('请选择角色')
        return
      }
      this.dialog.save = true
    },
    //保存
    async saveUser(rows) {
      const userIds = rows.map((v) => v.id)
      const params = { roleId: this.list.model.roleId, userIds }
      await api.add(params)
      await this.refresh()
      await this._success('保存成功')
    }
  }
}
</script>
