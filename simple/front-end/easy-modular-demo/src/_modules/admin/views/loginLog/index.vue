<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item prop="userCode">
          <el-input v-model="list.model.userCode" clearable :size="fontSize" placeholder="请输入账号" />
        </el-form-item>
        <el-form-item prop="userName">
          <el-input v-model="list.model.userName" clearable :size="fontSize" placeholder="请输入姓名" />
        </el-form-item>
        <el-form-item prop="result">
          <el-select v-model="list.model.result" placeholder="请选择登录状态" :size="fontSize" clearable>
            <el-option label="成功" :value="true"> </el-option>
            <el-option label="失败" :value="false"> </el-option>
          </el-select>
        </el-form-item>

        <el-form-item prop="loginTimeStart">
          <el-row :gutter="12">
            <el-col :span="12"
              ><el-date-picker v-model="list.model.loginTimeStart" type="date" value-format="yyyy-MM-dd" placeholder="开始日期" style="width:100%" :clearable="false"> </el-date-picker
            ></el-col>
            <el-col :span="12"
              ><el-date-picker v-model="list.model.loginTimeEnd" type="date" value-format="yyyy-MM-dd" placeholder="结束日期" style="width:100%" :clearable="false"> </el-date-picker
            ></el-col>
          </el-row>
        </el-form-item>
      </template>

      <!--状态-->
      <template v-slot:col-result="{ row }">
        <el-tag v-if="row.result === true" type="success">成功</el-tag>
        <el-tag v-if="row.result === false" type="danger">失败</el-tag>
      </template>
    </em-list>
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'

// 接口
const api = $api.admin.loginLog

export default {
  name: page.name,
  mixins: [mixins.list],
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        noOperateColumn: true,
        model: {
          //租户
          tenantId: '',
          //账号
          userCode: '',
          //姓名
          userName: '',
          //结果
          result: null,
          //登录时间(开始)
          loginTimeStart: this.$dayjs().add(-6, 'day').format('YYYY-MM-DD'),
          //登录时间(结束)
          loginTimeEnd: this.$dayjs().format('YYYY-MM-DD')
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
