<template>
  <em-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="名称：" prop="name">
        <el-input v-model="list.model.name" clearable />
      </el-form-item>
    </template>

    <!--操作栏-->
    <template v-slot:operatebar="{ total }">
      <em-button text="添加" type="success" icon="plus-circle" @click="add(total)" />
      <em-button text="导入" type="warning" icon="cloud-upload" @click="importShow = true" />
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{ row }">
      <em-button text="编辑" type="text" icon="edit" @click="edit(row)" />
      <em-button-delete type="text" icon="delete" :id="row.id" :action="removeAction" @success="onDelete(row)" />
    </template>

    <save-page :id="curr.id" :total="total" :parent-id="parentId" :dict="dict" :visible.sync="dialog.save" @success="onSave" />
    <!--导入-->
    <em-import-data :visible.sync="importShow" moduleCode="Demo.Admin" templateCode="dictionaryItem" :extra="list.model" :importApiUrl="importApiUrl" @success="refresh"></em-import-data>
  </em-list>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import cols from './cols'
import SavePage from '../save'

const api = $api.admin.dictionaryItem

export default {
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: '数据项列表',
        cols,
        action: api.query,
        operationWidth: '250',
        model: {
          //名称
          name: '',
          //父级Id
          parentId: this.$emptyGuid,
          //排序
          orderFileds: 'sort asc'
        }
      },
      removeAction: api.remove,
      importShow: false,
      importApiUrl: api.importUrl()
    }
  },
  props: {
    dict: Object,
    parentId: String
  },
  methods: {
    /**
     * @description: 刷新
     * @param {*}
     */
    refresh() {
      this.list.model.parentId = this.parentId
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },

    /**
     * @description: 字典项保存成功
     * @param {*} model
     * @param {*} data
     * @param {*} isAdd
     */
    onSave(model, data, isAdd) {
      const nodeData = { id: model.id || data.id, label: model.name, item: Object.assign({}, data) }
      if (isAdd) {
        this.$emit('add-success', nodeData)
      } else {
        this.$emit('edit-success', nodeData)
      }
      this.refresh()
    },

    /**
     * @description: 字典项删除成功
     * @param {*} row
     */
    onDelete(row) {
      this.$emit('del-success', row.id)
      this.refresh()
    }
  },
  watch: {
    parentId() {
      this.refresh()
    }
  }
}
</script>
