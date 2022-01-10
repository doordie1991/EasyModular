<template>
  <em-panel class="contacts-panel" page header footer footer-align="center" no-padding>
    <template v-slot:title>
      <el-input v-model="keywords" placeholder="请输入关键字" clearable @clear="search" @keyup.native="search" :size="fontSize">
        <el-button slot="append" icon="el-icon-search" @click="search"></el-button>
      </el-input>
    </template>

    <div class="user-list">
      <user-item v-for="item in rows" :key="item.id" :data="item" @click.native="addUser(item)"></user-item>
    </div>

    <template v-slot:footer>
      共{{ total }}条
      <el-pagination small background layout="prev, pager, next" :total="total" :page-size="page.size" :current-page.sync="page.index" @current-change="onCurrentChange"></el-pagination>
    </template>
  </em-panel>
</template>

<script>
import UserItem from '../user-item'
const { queryBySameOrg } = $api.admin.user
export default {
  components: { UserItem },
  data() {
    return {
      page: {
        index: 1,
        size: 15
      },
      rows: [],
      total: 0,
      keywords: ''
    }
  },
  props: {
    //已选择的人员列表
    selection: Array
  },
  methods: {
    /**
     * @description: 获取数据
     * @param {*}
     */
    async getData() {
      const params = { keywords: this.keywords, pageIndex: this.page.index, pageSize: this.page.size }
      const result = await queryBySameOrg(params)
      this.rows = result.rows
      this.total = result.total
    },

    /**
     * @description: 查询
     * @param {*}
     */
    search() {
      this.page.index = 1
      this.getData()
    },

    /**
     * @description: 分页
     * @param {*}
     */
    onCurrentChange() {
      this.getData()
    },
    /**
     * @description: 添加用户
     * @param {*} item
     */
    addUser(item) {
      if (this.selection.findIndex(m => m.id === item.id) >= 0) {
        this._error('已添加')
        return
      }
      this.selection.push(item)
    }
  }
}
</script>
