<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="后缀名：" prop="ext">
          <el-input v-model="list.model.ext" clearable placeholder="请输入后缀名" />
        </el-form-item>
        <el-form-item label="媒体类型：" prop="value">
          <el-input v-model="list.model.value" clearable placeholder="请输入媒体类型" />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
        <em-button v-bind="buttons.import" @click="importShow = true" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.edit" @click="edit(row)" />
        <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </em-list>

    <!--编辑-->
    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
    <!--导入-->
    <em-import-data :visible.sync="importShow" :importApiUrl="importApiUrl" @success="refresh"></em-import-data>
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../_components/save'

// 接口
const api = $api.admin.mediaType

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
          //后缀名
          ext: '',
          //媒体类型
          value: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      importShow: false,
      importApiUrl: api.importUrl()
    }
  }
}
</script>
