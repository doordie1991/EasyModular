<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="12">
        <el-form-item label="系统模块" prop="module">
          <em-module-select v-model="form.model.module" placeholder="请选择模块"></em-module-select>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="任务分组" prop="jobGroup">
          <job-group-select v-model="form.model.jobGroup" placeholder="请选择任务分组"></job-group-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="任务名称" prop="name">
          <el-input v-model="form.model.name" :size="fontSize" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="任务编码" prop="code">
          <el-input v-model="form.model.code" :size="fontSize" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="执行类" prop="jobClass">
          <template v-slot:label>
            <em-form-item-tip label="执行类" content="类全名,命名空间（如：Demo.Admin.Quartz.TestTask,Demo.Admin.Quartz）"></em-form-item-tip>
          </template>
          <el-input v-model="form.model.jobClass" :size="fontSize" placeholder="类全名,命名空间（如：Demo.Admin.Quartz.TestTask,Demo.Admin.Quartz）" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="触发器" prop="triggerType">
          <el-radio-group v-model="form.model.triggerType" :size="fontSize">
            <el-radio-button :label="0">通用</el-radio-button>
            <el-radio-button :label="1">CRON</el-radio-button>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="开始时间" prop="startTime">
          <el-date-picker v-model="form.model.startTime" type="date" placeholder="选择日期" style="width: 100%"></el-date-picker>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="结束时间" prop="endTime">
          <el-date-picker v-model="form.model.endTime" type="date" placeholder="选择日期" style="width: 100%"></el-date-picker>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row v-if="form.model.triggerType === 0">
      <el-col :span="12">
        <el-form-item label="间隔时间" prop="interval" :rules="[{ required: form.model.triggerType === 0, message: '请输入间隔时间', trigger: 'blur' }]">
          <el-input v-model.number="form.model.interval" placeholder="请输入执行时间间隔">
            <template slot="append">秒</template>
          </el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="重复次数" prop="repeatCount" :rules="[{ required: form.model.triggerType === 0, message: '请输入重复次数', trigger: 'blur' }]">
          <el-input v-model.number="form.model.repeatCount" placeholder="请输入重复次数，0表示无限次">
            <template slot="append">次</template>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row v-else>
      <el-col :span="24">
        <el-form-item label="CRON表达式" prop="cron" :rules="[{ required: form.model.triggerType === 1, message: '请输入CRON表达式', trigger: 'blur' }]">
          <el-input v-model="form.model.cron" placeholder="请输入CRON表达式"></el-input>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="24">
        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.model.remark" placeholder="请输入备注"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import JobGroupSelect from '../../../jobGroup/_components/select'

// 接口
const { add, edit, update } = $api.scheduling.job

export default {
  mixins: [mixins.formSave],
  components: { JobGroupSelect },
  data() {
    return {
      title: '任务',
      actions: { add, edit, update },
      form: {
        width: '900px',
        height: '500px',
        labelWidth: '120px',
        model: {
          //系统模块
          module: '',
          //任务分组
          jobGroup: '',
          //执行类
          jobClass: '',
          //任务名称
          name: '',
          //任务编码
          code: '',
          //触发器
          triggerType: 0,
          //时间间隔
          interval: null,
          //重复次数
          repeatCount: null,
          //cron表达式
          cron: '',
          //开始时间
          startTime: '',
          //结束时间
          endTime: '',
          //备注
          remark: ''
        },
        rules: {
          module: [{ required: true, message: '请选择模块', trigger: 'change' }],
          jobClass: [{ required: true, message: '请选择任务类', trigger: 'change' }],
          group: [{ required: true, message: '请选择任务组', trigger: 'change' }],
          name: [{ required: true, message: '请输入任务名称', trigger: 'blur' }],
          code: [{ required: true, message: '请输入任务编码', trigger: 'blur' }],
          triggerType: [{ required: true, message: '请选择触发器', trigger: 'change' }],
          startTime: [{ required: true, message: '请选择开始日期', trigger: 'change' }],
          endTime: [{ required: true, message: '请选择开始日期', trigger: 'change' }]
        }
      }
    }
  }
}
</script>
