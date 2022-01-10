<template>
  <div class="user-select">
    <el-select v-model="value_" placeholder="请输入账号或名称" filterable remote :remote-method="getData" :disabled="disabled" :size="fontSize">
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
      value_: '',
      data: []
    }
  },
  props: {
    value: String,
    disabled: {
      type: Boolean,
      default: false
    }
  },
  watch: {
    value(val) {
      this.getData(val)
      if (this.value_ != val) this.value_ = val
    },
    value_(val) {
      if (this.value != val) {
        this.$emit('input', val)
        const item = this.data.find(m => m.value == val)
        if (item) {
          this.$emit('update:name', item.label)
        }
      }
    }
  },
  methods: {
    async getData(val) {
      const params = { keywords: val }
      const data = await api.select(params)
      this.data = data
    }
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
