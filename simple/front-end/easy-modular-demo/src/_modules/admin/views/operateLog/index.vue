<template>
  <em-container>
    <em-list ref="list" v-bind="list" :tenantShow="tenantShow">
      <!--查询条件-->
      <template v-slot:querybar>
        
        <el-form-item prop="userCode">
          <el-input v-model="list.model.userCode" clearable :size="fontSize" placeholder="请输入账号" />
        </el-form-item>
        <el-form-item prop="userName">
          <el-input v-model="list.model.userName" clearable :size="fontSize" placeholder="请输入姓名" />
        </el-form-item>

        <el-form-item prop="operateTimeStart">
          <el-row :gutter="12">
            <el-col :span="12"><el-date-picker v-model="list.model.operateTimeStart" type="date" value-format="yyyy-MM-dd"  placeholder="开始日期" style="width:100%" :clearable="false"  > </el-date-picker></el-col>
            <el-col :span="12"><el-date-picker v-model="list.model.operateTimeEnd" type="date" value-format="yyyy-MM-dd" placeholder="结束日期" style="width:100%" :clearable="false"> </el-date-picker></el-col>
          </el-row>
        </el-form-item>
      </template>

      <!--状态-->
      <template v-slot:col-action="{ row }"> {{ row.controllerDesc }} {{ row.actionDesc }} </template>
    </em-list>
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'

// 接口
const api = $api.admin.auditInfo
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
          //租户Id
          tenantId: '',
          //账号
          userCode: '',
          //姓名
          userName: '',
          //操作时间(开始)
          operateTimeStart: this.$dayjs().add(-6, 'day').format('YYYY-MM-DD'),
          //操作时间(结束)
          operateTimeEnd: this.$dayjs().format('YYYY-MM-DD')
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
