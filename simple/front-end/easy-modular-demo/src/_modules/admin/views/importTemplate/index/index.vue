<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="moduleCode">
          <em-module-select v-model="list.model.moduleCode"></em-module-select>
        </el-form-item>
        <el-form-item label="模板名称：" prop="templateName">
          <el-input v-model="list.model.templateName" clearable placeholder="请输入模板名称" />
        </el-form-item>
        <el-form-item label="模板编码：" prop="templateCode">
          <el-input v-model="list.model.templateCode" clearable placeholder="请输入模板编码" />
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
import SavePage from '../_components/save'

// 接口
const api = $api.admin.importTemplate

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
        model: {
          //模块编码
          moduleCode: '',
          //模板编码
          templateCode: '',
          //模板名称
          templateName: '',
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
