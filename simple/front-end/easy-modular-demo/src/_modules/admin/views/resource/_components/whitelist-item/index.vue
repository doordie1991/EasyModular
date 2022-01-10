<template>
  <div :class="['whitelist-item', data.valueType === 0 ? 'user' : 'role']">
    <div class="whitelist-item-avatar">
      <em-icon :name="data.valueType === 0 ? 'user' : 'redis'" :color="data.valueType === 0 ? '#00a29a' : '#409eff'" />
    </div>
    <div class="whitelist-item-info" :title="info">{{ info }}</div>
    <div :class="['whitelist-item-del', data.valueType === 0 ? 'user' : 'role']" @click="onDelete">
      <em-icon name="close" :color="data.valueType === 0 ? '#00a29a' : '#409eff'" />
    </div>
  </div>
</template>

<script>
export default {
  props: {
    data: {
      type: Object,
      require: true
    }
  },
  data() {
    return {
      info: ''
    }
  },
  methods: {
    /**
     * @description: 获取数据
     * @param {*} id
     */
    async getData(id) {
      if (this.data.valueType === 0) {
        const result = await $api.admin.user.edit(id)
        this.info = `${result.userName}（${result.userCode}）`
      }
      if (this.data.valueType === 1) {
        const result = await $api.admin.role.edit(id)
        this.info = `${result.roleName}（${result.roleCode}）`
      }
    },

    /**
     * @description: 移除
     * @param {*}
     */
    onDelete() {
      this.$emit('delete', this.data)
    }
  },
  watch: {
    'data.value': {
      handler(n, o) {
        if (n && n != o) this.getData(n)
      },
      immediate: true
    }
  }
}
</script>

<style lang="scss" scoped>
.whitelist-item {
  position: relative;
  display: flex;
  align-items: center;
  width: 140px;
  height: 60px;
  padding: 4px;
  box-sizing: border-box;
  margin-right: 20px;
  margin-bottom: 20px;

  border-radius: 4px;
  cursor: pointer;

  &.user {
    border: 1px solid #00a29a;
  }
  &.role {
    border: 1px solid #409eff;
  }
  &:hover {
    background: #d3f5f3;
  }
}

.whitelist-item-avatar {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  font-size: 28px;
}
.whitelist-item-info {
  flex-grow: 1;
  line-height: 60px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.whitelist-item-del {
  position: absolute;
  right: -8px;
  top: -10px;
  width: 18px;
  height: 18px;
  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 50%;

  font-size: 12px;

  &.user {
    border: 1px solid #00a29a;
    background: #d3f5f3;
  }
  &.role {
    border: 1px solid #409eff;
    background: #e3effb;
  }
}
</style>
