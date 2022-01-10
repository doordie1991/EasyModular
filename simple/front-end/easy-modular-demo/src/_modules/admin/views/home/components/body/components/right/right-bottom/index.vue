<template>
  <em-panel class="em-home-body-right-bottom" v-bind="panel">
    <template v-slot:toolbar>
      <el-button size="mini" plain>更多</el-button>
    </template>

    <div class="message-list">
      <message-item v-for="(item, index) in messageList" :key="index" :type="item.type" :content="item.content" :time="item.time" :url="item.url"></message-item>
    </div>
  </em-panel>
</template>

<script>
import messageItem from '../message-item'
export default {
  components: { messageItem },
  data() {
    return {
      panel: {
        title: '消息 News',
        icon: 'message',
        iconColor: '#07c160',
        height: '386px',
        header: true,
        noPadding: true
      },
      messageList: [
        { type: '系统公告', content: '系统维护时间公告', time: '2021-12-07 12:12', url: '' },
        { type: '系统待办', content: '张三请假申请审批', time: '2021-12-07 12:12', url: '' },
        { type: '系统待办', content: '李四加班申请审批', time: '2021-12-07 12:12', url: '' },
        { type: '生日提醒', content: '今天有2位员工生日，请祝他们生日快乐', time: '2021-12-07 12:12', url: '' },
        { type: '危险作业', content: 'A厂区动火作业开工', time: '2021-12-07 12:12', url: '' },
        { type: '消防演习', content: 'B厂区化学品泄漏演习', time: '2021-12-07 12:12', url: '' },
        { type: '系统公告', content: '数据库升级公告', time: '2021-12-07 12:12', url: '' }
      ]
    }
  }
}
</script>

<style lang="scss">
.em-home-body-right-bottom {
  width: 100%;
  margin-top: 16px;
}
</style>
