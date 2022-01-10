<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <em-panel header title="基本信息" icon="appstore">
      <el-row>
        <el-col :span="24">
          <el-form-item label="模块" prop="module">
            <em-module-select v-model="form.model.module"></em-module-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="资源名称" prop="name">
            <el-input v-model="form.model.name" clearable autofocus :size="fontSize" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="资源编码" prop="code">
            <el-input v-model="form.model.code" clearable autofocus :size="fontSize" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="数据来源" prop="source">
            <el-radio v-model="form.model.source" :label="0">手动</el-radio>
            <el-radio v-model="form.model.source" :label="1">自动</el-radio>
          </el-form-item>
        </el-col>
      </el-row>
    </em-panel>
    <em-panel header title="明细" icon="detail" style="margin-top: 4px">
      <el-form-item prop="details" style="display: none"> </el-form-item>
      <el-table :data="form.model.details" style="width: 100%" border :size="fontSize">
        <el-table-column prop="fieldName" label="字段名称" align="center">
          <template slot-scope="scope">
            <el-input :size="fontSize" v-model="scope.row.fieldName" placeholder="请输入字段名称" />
          </template>
        </el-table-column>
        <el-table-column prop="fieldType" label="字段类型" align="center">
          <template slot-scope="scope">
            <el-input :size="fontSize" v-model="scope.row.fieldType" placeholder="请输入字段类型" />
          </template>
        </el-table-column>
        <el-table-column label="操作" align="center" width="100">
          <template slot-scope="scope">
            <em-button type="text" icon="delete" @click="deleteDetail(scope.$index)">移除</em-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="add-btn">
        <em-button type="primary" icon="add" size="mini" @click="addDetail">新增</em-button>
      </div>
    </em-panel>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const { add, edit, update } = $api.admin.resource

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '资源',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '900px',
        height: '700px',
        labelWidth: '130px',
        padding: 0,
        model: {
          //模块
          module: '',
          //资源编码
          code: '',
          //资源名称
          name: '',
          //数据来源（0:手动、1:自动）
          source: 0,
          //明细
          details: []
        },
        rules: {
          module: [{ required: true, message: '请输入模块' }],
          code: [{ required: true, message: '请输入资源编码' }],
          name: [{ required: true, message: '请输入资源名称' }],
          source: [{ required: true, message: '请选择数据来源' }]
        }
      }
    }
  },
  methods: {
    /**
     * @description: 移除
     * @param {*}
     */
    deleteDetail(index) {
      this.form.model.details.splice(index, 1)
    },

    /**
     * @description: 添加
     * @param {*}
     */
    addDetail() {
      this.form.model.details.push({ fieldName: '', fieldType: '' })
    }
  }
}
</script>

<style lang="scss" scoped>
.add-btn {
  display: flex;
  width: 100%;
  height: 60px;
  justify-content: center;
  align-items: center;
  border: 1px solid #d8d9da;
  border-radius: 6px;
  margin-top: 12px;
  box-shadow: 0px 0px 8px #bec2c5;
}
</style>
