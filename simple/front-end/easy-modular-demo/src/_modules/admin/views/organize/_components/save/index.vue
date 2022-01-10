<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="路径">
          <el-input v-model="parent.path" disabled :size="fontSize" />
        </el-form-item>
        <el-form-item label="组织名称" prop="organizeName">
          <el-input ref="organizeName" v-model="form.model.organizeName" clearable :size="fontSize" />
        </el-form-item>
        <el-form-item label="组织编码" prop="organizeCode">
          <el-input v-model="form.model.organizeCode" clearable :size="fontSize" />
        </el-form-item>
        <el-form-item label="组织排序" prop="sort">
          <el-input-number v-model="form.model.sort" :size="fontSize" />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

const { add, edit, update } = $api.admin.organize

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '组织',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '600px',
        model: {
          //父级组织
          parentId: '',
          //组织名称
          organizeName: '',
          //组织编码
          organizeCode: '',
          //排序
          sort: 0
        },
        rules: {
          organizeCode: [{ required: true, message: '请输入组织编码' }],
          organizeName: [{ required: true, message: '请输入组织名称' }]
        }
      },
      on: {
        reset: this.onReset
      }
    }
  },
  props: ['parent', 'total'],
  methods: {
    onReset() {
      this.form.model.parentId = this.parent.id || this.$emptyGuid
      this.form.model.sort = this.total
      this.$refs.organizeName.focus()
    }
  }
}
</script>
