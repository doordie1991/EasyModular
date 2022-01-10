<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="分组名称：" prop="name">
          <el-input v-model="list.model.name" clearable placeholder="请输入分组名称" />
        </el-form-item>
        <el-form-item label="分组编码：" prop="code">
          <el-input v-model="list.model.code" clearable placeholder="请输入分组编码" />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
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
import savePage from '../_components/save'

// 接口
const api = $api.scheduling.jobGroup

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { savePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          //分组编码
          code: '',
          //分组名称
          name: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
