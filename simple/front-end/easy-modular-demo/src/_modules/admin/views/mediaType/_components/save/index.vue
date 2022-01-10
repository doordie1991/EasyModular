<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="扩展名" prop="ext">
          <el-input v-model="form.model.ext" :size="fontSize" />
        </el-form-item>
        <el-form-item label="媒体类型" prop="value">
          <el-input v-model="form.model.value" :size="fontSize" />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.mediaType

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '媒体类型',
      actions: { add, edit, update },
      form: {
        width: '500px',
        height: '250px',
        model: {
          //扩展名
          ext: '',
          //媒体类型
          value: ''
        },
        rules: {
          ext: [{ required: true, message: '请输入扩展名' }],
          value: [{ required: true, message: '请输入媒体类型' }]
        }
      }
    }
  }
}
</script>
