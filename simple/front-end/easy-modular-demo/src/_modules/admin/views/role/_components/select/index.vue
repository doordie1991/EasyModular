<template>
  <div class="role-select">
    <el-select v-model="value_" multiple placeholder="请选择">
      <el-option
        v-for="item in options"
        :key="item.id"
        :label="item.roleName"
        :value="item.id"
      >
      </el-option>
    </el-select>
  </div>
</template>

<script>
const { query } = $api.admin.role
export default {
  props: { value: Array },
  data() {
    return {
      options: [],
      value_: []
    }
  },
  methods: {
    async getData() {
      const data = await query({ pageSize: 1000 })
      this.options = data.rows
    }
  },
  watch: {
    value(n, o) {
      if (n != o) {
        this.value_ = n
      }
    },
    value_(n, o) {
      if (n != this.value) this.$emit("input", n)
    }
  },
  created() {
    this.getData()
  }
}
</script>
