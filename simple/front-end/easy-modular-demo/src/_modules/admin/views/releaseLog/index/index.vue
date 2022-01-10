<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="版本号：" prop="version">
          <el-input v-model="list.model.version" clearable placeholder="请输入版本号" />
        </el-form-item>
        <el-form-item label="发布内容：" prop="content">
          <el-input v-model="list.model.content" clearable placeholder="请输入发布内容" />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <template v-slot:col-releaseTime="{ row }">
        {{ row.releaseTime | dateFormat }}
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
const api = $api.admin.releaseLog

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
          //版本号
          version: '',
          //发布内容
          content: '',
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
