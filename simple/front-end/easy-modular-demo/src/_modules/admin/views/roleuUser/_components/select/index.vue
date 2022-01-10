<template>
  <div class="user-select">
    <el-select v-model="value_" :placeholder="placeholder" :disabled="disabled" :size="fontSize">
      <el-option v-for="item in data" :key="item.value" :label="item.label" :value="item.value">
        <span class="user-l">{{ item.label }}</span>
        <span class="user-r">{{ item.data.userCode }}</span>
      </el-option>
    </el-select>
  </div>
</template>
<script>
const api = $api.admin.roleUser
export default {
  data() {
    return {
      value_: '',
      data: []
    }
  },
  props: {
    value: String,
    //角色编码
    code: {
      type: String,
      required: true
    },
    placeholder: {
      type: String,
      default: '请选择'
    },
    disabled: {
      type: Boolean
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
    async getData() {
      const params = { roleCode: this.code }
      const data = await api.select(params)
      this.data = data
    }
  },
  created() {
    this.getData()
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
