<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable />
        </el-form-item>
        <el-form-item label="图标：" prop="icon">
          <em-icon-picker v-model="form.model.icon" />
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input type="number" v-model.number="form.model.sort" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.dictionaryGroup

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '字典分组',
      actions: { add, edit, update },
      form: {
        width: '500px',
        model: {
          //名称
          name: '',
          //编码
          code: '',
          //图标
          icon: '',
          //排序
          sort: 0
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          code: [{ required: true, message: '请输入编码' }],
          sort: [
            { required: true, message: '请输入序号' },
            { type: 'number', message: '请输入数字' }
          ]
        }
      }
    }
  }
}
</script>
