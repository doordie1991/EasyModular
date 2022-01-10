<template>
  <em-panel class="em-home-body-right-top" v-bind="panel">
    <template v-slot:toolbar>
      <el-button size="mini" plain @click="itemActive = !itemActive">管理</el-button>
    </template>
    <div class="app-list">
      <app-item v-for="(item, index) in appList" :data="item" :key="index" :active="itemActive"></app-item>
      <app-item-add></app-item-add>
    </div>
  </em-panel>
</template>

<script>
import appItem from '../app-item'
import appItemAdd from '../app-item-add'
export default {
  components: { appItem, appItemAdd },
  data() {
    return {
      panel: {
        title: '快捷应用',
        icon: 'appstore',
        iconColor: '#1989fa',
        height: '236px',
        header: true,
        noPadding: true
      },
      appList: [
        { label: '车辆违规', icon: 'car', url: '', target: '', color: '#07c160' },
        { label: '消防演习', icon: 'flag', url: '', target: '', color: '#1989fa' },
        { label: '危险作业', icon: 'fire-fill', url: '', target: '', color: '#ff9900' },
        { label: '事件快报', icon: 'alert-fill', url: '', target: '', color: '#f56c6c' },
        { label: '人员管理', icon: 'user', url: '', target: '', color: '#00acc1' },
        { label: '车辆违规', icon: 'appstore', url: '', target: '', color: '#07c160' },
        { label: '消防演习', icon: 'tags', url: '', target: '', color: '#1989fa' },
        { label: '人员管理', icon: 'transaction', url: '', target: '', color: '#00acc1' }
      ],
      itemActive: false
    }
  }
}
</script>

<style lang="scss" scoped>
.app-list {
  display: flex;
  flex-wrap: wrap;
  padding: 16px 16px 0px 16px;
  box-sizing: border-box;
  width: 100%;
}
</style>
