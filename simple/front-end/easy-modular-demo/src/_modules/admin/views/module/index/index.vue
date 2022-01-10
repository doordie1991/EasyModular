<template>
  <em-panel class="module-list-wrapper" title="系统模块" icon="appstore" header page>
    <div class="module-list">
      <module-item v-for="(item, index) in modules" :key="index" :data="item"></module-item>
    </div>
  </em-panel>
</template>

<script>
import ModuleItem from '../_components/module-item'

const { query } = $api.admin.modules

export default {
  components: { ModuleItem },
  data() {
    return {
      modules: [],
      list: {
        model: {
          //当前页
          pageIndex: 1,
          //页大小
          pageSize: 50,
          //总数
          total: 0,
          //排序
          orderFileds: 'sort asc'
        }
      }
    }
  },
  methods: {
    async getData() {
      const result = await query(this.list.model)
      this.modules = result.rows
    }
  },
  created() {
    this.getData()
  }
}
</script>

<style lang="scss" scoped>
.module-list-wrapper {
  .module-list {
    display: flex;
    flex-wrap: wrap;
  }
  /deep/ .el-scrollbar__view {
    padding: 16px !important;
  }
}
</style>
