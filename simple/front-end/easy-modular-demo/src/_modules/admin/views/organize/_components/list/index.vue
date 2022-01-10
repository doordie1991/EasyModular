<template>
  <em-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="组织名称：" prop="organizeName">
        <el-input v-model="list.model.organizeName" clearable />
      </el-form-item>
      <el-form-item label="组织编码：" prop="organizeCode">
        <el-input v-model="list.model.organizeCode" clearable />
      </el-form-item>
    </template>

    <!--操作栏-->
    <template v-slot:operatebar="{ total }">
      <em-button v-bind="buttons.add" @click="add(total)" />
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{ row }">
      <em-button v-bind="buttons.edit" @click="edit(row)" />
      <em-button v-bind="buttons.move" @click="onMove(row)" />
      <em-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="onRemoveSuccess(row.id)" />
    </template>

    <!--保存页-->
    <save-page :id="curr.id" :total="total" :parent="parent" :visible.sync="dialog.save" @success="onSaveSuccess" />
    <!--组织移动-->
    <organize-move :id="curr.id" :visible.sync="organizeMoveShow" @success="onMoveSuccess" />
  </em-list>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from '../../index/page'
import cols from './cols'
import SavePage from '../save'
import OrganizeMove from '../move'

const { query, remove } = $api.admin.organize
export default {
  mixins: [mixins.list],
  components: { SavePage, OrganizeMove },
  data() {
    return {
      list: {
        title: '组织架构',
        cols,
        action: query,
        model: {
          //父级组织
          parentId: this.$emptyGuid,
          //组织编码
          organizeCode: '',
          //组织名称
          organizeName: ''
        }
      },
      removeAction: remove,
      buttons: page.buttons,
      organizeMoveShow: false
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
     * @description:删除成功
     * @param {*} id
     */
    onRemoveSuccess(id) {
      this.$emit('remove-success', id)
    },

    /**
     * @description: 移动
     * @param {*} row
     */
    onMove(row) {
      this.curr.id = row.id
      this.organizeMoveShow = true
    },

    /**
     * @description: 移动成功
     * @param {*}
     */
    onMoveSuccess() {
      this.$emit('move-success')
    }
  }
}
</script>
