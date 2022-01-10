<template>
  <em-container>
    <em-split v-model="split">
      <template v-slot:left>
        <org-tree ref="tree" @change="onSelectOrg" />
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

          <!--性别-->
          <template v-slot:col-sex="{ row }">
            <el-tag v-if="row.sex" type="success">男</el-tag>
            <el-tag v-else type="danger">女</el-tag>
          </template>

          <!--状态-->
          <template v-slot:col-status="{ row }">
            <el-tag v-if="row.status === 0">启用</el-tag>
            <el-tag v-else-if="row.status === 1" type="success">启用</el-tag>
            <el-tag v-else type="danger">禁用</el-tag>
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{ row }">
            <em-button v-bind="buttons.edit" @click="edit(row)" />
            <em-button v-bind="buttons.enable" @click="enable(row)" />
            <em-button v-bind="buttons.disable" @click="disable(row)" />
            <em-button v-bind="buttons.move" @click="onMove(row)" />
            <em-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="refresh" />
          </template>

          <!--保存页-->
          <save-page :id="curr.id" :total="total" :organizeId="curOrganizeId" :visible.sync="dialog.save" @success="refresh" />
          <!--用户移动-->
          <user-move :id="curr.id" :visible.sync="userMoveShow" @success="onMoveSuccess" />
        </em-list>
      </template>
    </em-split>
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../_components/save'
import UserMove from '../_components/move'
import OrgTree from '../../organize/_components/tree'

// 接口
const api = $api.admin.user

export default {
  mixins: [mixins.list],
  components: { SavePage, UserMove, OrgTree },
  data() {
    return {
      split: 0.2,
      curOrganizeId: this.$emptyGuid,
      list: {
        title: '用户管理',
        operationWidth: '320',
        cols,
        action: api.query,
        model: {
          //用户账号
          userCode: '',
          //用户名称
          userName: '',
          //组织Id
          organizeId: this.$emptyGuid,
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      userMoveShow: false
    }
  },
  methods: {
    /**
     * @description: 启用
     * @param {*} row
     */
    async enable(row) {
      await this._confirm('您确定要启用该用户吗?')
      await api.enable(row.id)
      await this._success('启用成功')
      this.refresh()
    },

    /**
     * @description: 禁用
     * @param {*} row
     */
    async disable(row) {
      await this._confirm('您确定要禁用该用户吗?')
      await api.disable(row.id)
      await this._success('禁用成功')
      this.refresh()
    },

    /**
     * @description: 选择组织触发事件
     * @param {*} data
     */
    async onSelectOrg(data) {
      this.curOrganizeId = data.id
      this.list.model.organizeId = data.id
      this.refresh()
    },

    /**
     * @description: 更新组织
     * @param {*} row
     */
    onMove(row) {
      this.curr.id = row.id
      this.userMoveShow = true
    },

    /**
     * @description: 移动成功
     * @param {*}
     */
    onMoveSuccess() {
      this.refresh()
    }
  }
}
</script>
