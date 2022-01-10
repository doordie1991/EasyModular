<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="角色编码" prop="roleCode">
          <el-input v-model="form.model.roleCode" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="角色名称" prop="roleName">
          <el-input v-model="form.model.roleName" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="备注" prop="remark">
          <el-input type="textarea" v-model="form.model.remark" />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.role

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '角色',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '30%',
        model: {
          //数据行Id
          id: '',
          //角色编码
          roleCode: '',
          //角色名称
          roleName: '',
          //备注
          remark: ''
        },
        rules: {
          roleCode: [{ required: true, message: '请输入角色编码', trigger: 'blur' }],
          roleName: [{ required: true, message: '请输入角色名称', trigger: 'blur' }]
        }
      }
    }
  }
}
</script>
