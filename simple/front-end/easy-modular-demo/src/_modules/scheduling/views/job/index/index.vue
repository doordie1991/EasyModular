<template>
  <em-container>
    <em-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="module">
          <em-module-select v-model="list.model.module" placeholder="请选择模块"></em-module-select>
        </el-form-item>
        <el-form-item label="分组：" prop="jobGroup">
          <job-group-select v-model="list.model.jobGroup" placeholder="请选择任务分组"></job-group-select>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--操作栏-->
      <template v-slot:operatebar="{ total }">
        <em-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <template v-slot:col-triggerType="{ row }">
        <el-tag v-if="row.triggerType === 0" size="small">通用</el-tag>
        <el-tag v-else type="warning" size="small">CRON</el-tag>
      </template>

      <template v-slot:col-status="{ row }">
        <el-tag v-if="row.status === 0" type="info" effect="dark" size="small">停止</el-tag>
        <el-tag v-else-if="row.status === 1" type="success" effect="dark" size="small">运行中</el-tag>
        <el-tag v-else-if="row.status === 2" type="warning" effect="dark" size="small">暂停</el-tag>
        <el-tag v-else-if="row.status === 3" type="primary" effect="dark" size="small">已完成</el-tag>
        <el-tag v-else-if="row.status === 4" type="danger" effect="dark" size="small">异常</el-tag>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <em-button v-bind="buttons.edit" @click="edit(row)" />
        <em-button v-bind="buttons.pause" v-if="row.status === 1" @click="pause(row)" />
        <em-button v-bind="buttons.resume" v-if="[0, 2, 3].includes(row.status)" @click="resume(row)" />
        <em-button v-bind="buttons.stop" v-if="[1, 2].includes(row.status)" @click="stop(row)" />
        <em-button v-bind="buttons.log" @click="log(row)" />
        <em-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </em-list>

    <!--编辑-->
    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
    <!--日志-->
    <log-list :id="curr.id" :name="curr.name" :visible.sync="logShow" />
  </em-container>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import page from './page'
import cols from './cols'
import savePage from '../_components/save'
import logList from '../_components/log-list'
import jobGroupSelect from '../../jobGroup/_components/select'

// 接口
const api = $api.scheduling.job

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { savePage, logList, jobGroupSelect },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '300px',
        model: {
          //模块
          module: '',
          //任务分组
          jobGroup: '',
          //名称
          name: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      logShow: false
    }
  },
  methods: {
    /**
     * @description: 暂停
     * @param {*} row
     */
    pause(row) {
      this._confirm(`您确定要暂停《${row.name}》任务吗?`, '提醒').then(() => {
        this._openLoading(`正在暂停任务《${row.name}》，请稍后...`)
        api
          .pause(row.id)
          .then(() => {
            row.status = 2
            this._closeLoading()
          })
          .catch(() => {
            this._closeLoading()
          })
      })
    },

    /**
     * @description: 启用
     * @param {*} row
     */
    resume(row) {
      this._confirm(`您确定要启动《${row.name}》任务吗?`, '提醒').then(() => {
        this._openLoading(`正在启动任务《${row.name}》，请稍后...`)
        api
          .resume(row.id)
          .then(() => {
            row.status = 1
            this._closeLoading()
          })
          .catch(() => {
            this._closeLoading()
          })
      })
    },

    /**
     * @description: 停止
     * @param {*} row
     */
    stop(row) {
      this._confirm(`您确定要停止《${row.name}》任务吗?`, '警告', 'danger').then(() => {
        this._openLoading(`正在停止任务《${row.name}》，请稍后...`)
        api
          .stop(row.id)
          .then(() => {
            row.status = 0
            this._closeLoading()
          })
          .catch(() => {
            this._closeLoading()
          })
      })
    },

    /**
     * @description: 日志
     * @param {*} row
     */
    log(row) {
      this.curr = row
      this.logShow = true
    }
  }
}
</script>
