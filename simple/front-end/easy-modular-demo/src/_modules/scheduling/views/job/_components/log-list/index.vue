<template>
  <em-dialog :title="`任务日志(${name})`" v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="开始日期：" prop="startTime">
          <el-date-picker v-model="list.model.startTime" type="date" value-format="yyyy-MM-dd" placeholder="选择日期"></el-date-picker>
        </el-form-item>
        <el-form-item label="结束日期：" prop="endTime">
          <el-date-picker v-model="list.model.endTime" type="date" value-format="yyyy-MM-dd" placeholder="选择日期"></el-date-picker>
        </el-form-item>
      </template>

      <template v-slot:col-type="{ row }">
        <el-tag v-if="row.type === 0" type="success" effect="dark" size="small">信息</el-tag>
        <el-tag v-else-if="row.type === 1" type="warning" effect="dark" size="small">调试</el-tag>
        <el-tag v-else type="danger" effect="dark" size="small">异常</el-tag>
      </template>
    </em-list>
  </em-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import cols from './cols'

const api = $api.scheduling.jobLog

export default {
  mixins: [mixins.visible],
  data() {
    return {
      dialog: {
        width: '80%',
        height: '70%',
        icon: 'detail',
        padding: 0,
        noScrollbar:true
      },
      list: {
        noHeader: true,
        noOperation: true,
        cols,
        action: api.query,
        model: {
          jobId: this.$emptyGuid,
          startTime: this.$dayjs().add(-6, 'day').format('YYYY-MM-DD'),
          endTime: this.$dayjs().format('YYYY-MM-DD'),
          orderFileds: 'createdTime desc'
        }
      }
    }
  },
  props: {
    id: String,
    name: String
  },
  methods: {
    refresh() {
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },
    onOpen() {
      this.list.model.jobId = this.id
      this.refresh()
    }
  }
}
</script>
