<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="12">
        <el-form-item label="用户名称" prop="userName">
          <el-input v-model="form.model.userName" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="用户账号" prop="userCode">
          <el-input v-model="form.model.userCode" clearable autofocus />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="手机号码" prop="phone">
          <el-input v-model="form.model.phone" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="form.model.email" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="性别" prop="sex">
          <el-radio v-model="form.model.sex" :label="true">男</el-radio>
          <el-radio v-model="form.model.sex" :label="false">女</el-radio>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="职位名称" prop="jobName">
          <em-dict-select group="admin" code="job" v-model="form.model.jobName"></em-dict-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="密码" prop="password">
          <el-input v-model="form.model.password" clearable autofocus />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="状态" prop="status">
          <el-radio v-model="form.model.status" :label="0">启用</el-radio>
          <el-radio v-model="form.model.status" :label="1">禁用</el-radio>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="24">
        <el-form-item label="角色" prop="roleIds">
          <role-select v-model="form.model.roleIds"></role-select>
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import RoleSelect from '../../../role/_components/select'

const { add, edit, update } = $api.admin.user

export default {
  mixins: [mixins.formSave],
  components:{RoleSelect},
  data() {
    return {
      title: '用户',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '800px',
        height:'500px',
        model: {
          userCode: '',
          userName: '',
          phone: '',
          email: '',
          sex: '',
          jobName: '',
          status: 0,
          organizeId: this.$emptyGuid,
          password: 'edcp123456',
          roleIds:[]
        },
        rules: {
          userCode: [{ required: true, message: '请输入用户账号' }],
          userName: [{ required: true, message: '请输入用户名称' }],
          sex: [{ required: true, message: '请输选择性别' }]
        }
      },
      on: {
        reset: this.onReset
      }
    }
  },
  props: ['organizeId'],
  methods: {
    onReset() {
      this.form.model.organizeId = this.organizeId
    }
  }
}
</script>
