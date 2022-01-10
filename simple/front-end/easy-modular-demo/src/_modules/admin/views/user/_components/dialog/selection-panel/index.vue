<template>
  <em-panel class="em-user-selection" page header no-padding :loading="loading">
    <template v-slot:title>
      <el-row>
        <el-col :span="18">
          <el-input v-model="keyword" placeholder="请输入姓名" clearable suffix-icon="el-icon-search" :size="fontSize"></el-input>
        </el-col>
        <el-col :span="6" class="em-user-selection-tip"> 已选{{ selection.length }}位 </el-col>
      </el-row>
    </template>

    <div class="user-list">
      <user-item v-for="item in selection" :key="item.id" :data="item" @onDelete="delUser" deleteBtn></user-item>
    </div>
  </em-panel>
</template>
<script>
import UserItem from '../user-item'
export default {
  components: { UserItem },
  data() {
    return {
      keyword: '',
      filterTimer: null,
      loading: false
    }
  },
  props: {
    selection: {
      type: Array,
      default: () => []
    }
  },
  methods: {
    search() {},
    /**
     * @description:删除用户
     * @param {*}
     */
    delUser(item) {
      const index = this.selection.findIndex(m => m.id === item.id)
      this.selection.splice(index, 1)
    }
  },
  watch: {
    keyword: {
      immediate: true,
      handler() {
        this.search()
      }
    }
  }
}
</script>
<style lang="scss">
.em-user-selection-tip {
  text-align: right;
  padding-right: 2px;
  letter-spacing: 2px;
  color: #f56c6c;
}
</style>
