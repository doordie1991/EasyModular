<template>
  <div class="user-select">
    <el-select v-model="value_" placeholder="请输入账号或名称" multiple filterable remote :remote-method="getData">
      <el-option v-for="item in data" :key="item.value" :label="item.label" :value="item.value">
        <span class="user-l">{{ item.label }}</span>
        <span class="user-r">{{ item.value }}</span>
      </el-option>
    </el-select>
  </div>
</template>
<script>
const api = $api.admin.user
export default {
  data() {
    return {
      value_: [],
      data: []
    }
  },
  props: {
    value: Array
  },
  watch: {
    value: {
      async handler(n, o) {
        if (!n) this.value_ = []
        else if (n != o) this.value_ = n.split(',')
      },
      immediate: true
    },
    
    async value_(val) {
      if (this.value != val.join(',')) {
        this.$emit('input', val.join(','))
        const users = await api.queryByUserCodes(val)
        this.$emit('update:name', users.map(m => m.userName).join(','))
      }
    }
  },
  methods: {
    async getData(val) {
      const params = { keywords: val }
      const data = await api.select(params)
      this.data = data
    }
  },
  async created() {
    await this.getData(this.value)
  }
}
</script>
<style lang="scss" scoped>
.user-l {
  float: left;
}
.user-r {
  margin-left: 20px;
  color: #409eff;
  font-size: 13px;
}
</style>
