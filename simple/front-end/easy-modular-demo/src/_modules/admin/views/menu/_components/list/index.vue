<template>
  <em-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="菜单名称：" prop="menuName">
        <el-input v-model="list.model.menuName" clearable />
      </el-form-item>
    </template>

    <!--操作栏-->
    <template v-slot:operatebar="{ total }">
      <em-button v-bind="buttons.add" @click="add(total)" />
    </template>

    <!--菜单类型-->
    <template v-slot:col-menuType="{ row }">
      <el-tag v-if="row.menuType === 0" type="success">节点</el-tag>
      <el-tag v-if="row.menuType === 1" type="primary">路由</el-tag>
      <el-tag v-if="row.menuType === 2" type="danger">链接</el-tag>
    </template>

    <!--是否显示-->
    <template v-slot:col-isShow="{ row }">
      <el-tag v-if="row.isShow" type="success">是</el-tag>
      <el-tag v-else type="danger">否</el-tag>
    </template>

    <!--是否受控-->
    <template v-slot:col-isControl="{ row }">
      <el-tag v-if="row.isControl" type="success">是</el-tag>
      <el-tag v-else type="danger">否</el-tag>
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{ row }">
      <em-button v-bind="buttons.edit" @click="edit(row)" />
      <em-button v-bind="buttons.bindBtn" @click="bindBtn(row)" />
      <em-button v-bind="buttons.move" @click="onMove(row)" />
      <em-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="onRemoveSuccess(row.id)" />
    </template>

    <!--保存页-->
    <save-page :id="curr.id" :total="total" :parent="parent" :visible.sync="dialog.save" @success="onSaveSuccess" />
    <!--菜单按钮-->
    <menu-button :visible.sync="buttonDialogShow" :menuId="menuId" :menuCode="menuCode" />
    <!--菜单移动-->
    <menu-move :id="curr.id" :visible.sync="moveDialogShow" />
  </em-list>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from '../../index/page'
import cols from './cols'
import savePage from '../save'
import menuButton from '../button'
import menuMove from '../move'

const { query, remove } = $api.admin.menu
export default {
  mixins: [mixins.list],
  components: { savePage, menuButton, menuMove },
  data() {
    return {
      menuId: '',
      menuCode: '',
      buttonDialogShow: false,
      moveDialogShow: false,
      list: {
        title: '菜单列表',
        operationWidth: '320',
        cols,
        action: query,
        model: {
          //父级菜单
          parentId: this.$emptyGuid,
          //菜单名称
          menuName: '',
          //排序
          orderFileds: 'sort asc'
        }
      },
      removeAction: remove,
      buttons: page.buttons
    }
  },
  props: ['parent'],
  methods: {
    /**
     * @description: 刷新
     * @param {*}
     */
    refresh() {
      this.$nextTick(() => {
        this.list.model.parentId = this.parent.id
        this.$refs.list.refresh()
      })
    },

    /**
     * @description: 移动
     * @param {*} row
     */
    onMove(row) {
      this.curr.id = row.id
      this.moveDialogShow = true
    },

    /**
     * @description: 保存成功
     * @param {*} model
     * @param {*} data
     * @param {*} isAdd
     */
    onSaveSuccess(model, data, isAdd) {
      if (isAdd) {
        model.id = data.id
      }
      this.$emit('save-success', model, data, isAdd)
    },

    /**
     * @description: 删除成功
     * @param {*} model
     * @param {*} data
     * @param {*} isAdd
     */
    onRemoveSuccess(id) {
      this.$emit('remove-success', id)
    },

    /**
     * @description: 按钮权限设置
     * @param {*} row
     */
    bindBtn(row) {
      this.menuId = row.id
      this.menuCode = row.menuCode
      this.buttonDialogShow = true
    }
  }
}
</script>
