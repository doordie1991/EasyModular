<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="版本号" prop="version">
          <el-input v-model="form.model.version" :size="fontSize" />
        </el-form-item>
        <el-form-item label="发布内容" prop="content">
          <el-input type="textarea" :rows="5" v-model="form.model.content" :size="fontSize" />
        </el-form-item>
        <el-form-item label="发布时间" prop="releaseTime">
          <el-date-picker v-model="form.model.releaseTime" type="date" placeholder="选择日期" style="width:100%"> </el-date-picker>
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.releaseLog

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '发布记录',
      actions: { add, edit, update },
      form: {
        width: '600px',
        height: '400px',
        labelPosition:'left',
        model: {
          //版本号
          version: '',
          //发布内容
          content: '',
          //发布时间
          releaseTime: this.$dayjs().format('YYYY-MM-DD')
        },
        rules: {
          version: [{ required: true, message: '请输入版本号' }],
          content: [{ required: true, message: '请输入发布内容' }],
          releaseTime: [{ required: true, message: '请选择发布时间' }]
        }
      }
    }
  }
}
</script>
