<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="分组编码" prop="code">
          <el-input v-model="form.model.code" :size="fontSize" />
        </el-form-item>
        <el-form-item label="分组名称" prop="name">
          <el-input v-model="form.model.name" :size="fontSize" />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.scheduling.jobGroup

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '任务分组',
      actions: { add, edit, update },
      form: {
        width: '500px',
        height: '250px',
        model: {
          //分组编码
          code: '',
          //分组名称
          name: ''
        },
        rules: {
          code: [{ required: true, message: '请输入分组编码' }],
          name: [{ required: true, message: '请输入分组名称' }]
        }
      }
    }
  }
}
</script>
