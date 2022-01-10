<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="分组：" prop="groupCode">
          <group-select v-model="list.model.groupCode" clearable />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="list.model.code" clearable />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.item" @click="manageItem(row)" />
        <em-button v-bind="buttons.edit" @click="edit(row)" />
        <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </em-list>

    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
    <!--管理数据项-->
    <dict-item :dict="curr" :visible.sync="dialog.item" />
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import GroupSelect from '../../dictionaryGroup/_components/select'
import SavePage from '../_components/save'
import DictItem from '../../dictionaryItem'

// 接口
const api = $api.admin.dictionary

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage, GroupSelect, DictItem },
  data() {
    return {
      curr: { code: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          //分组编码
          groupCode: '',
          //字典名称
          name: '',
          //字典编码
          code: '',
          //排序
          orderFileds: 'name asc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      dialog: {
        item: false
      }
    }
  },
  methods: {
    manageItem(row) {
      this.curr = row
      this.dialog.item = true
    }
  }
}
</script>
