<template>
  <div class="app-item" ref="appItem">
    <div class="app-item-icon">
      <em-icon :name="data.icon" :color="data.color"></em-icon>
    </div>
    <div class="app-item-label">{{ data.label }}</div>
    <div v-if="active" class="app-item-delete"><em-icon name="minus-circle" color="#f56c6c"></em-icon></div>
  </div>
</template>

<script>
export default {
  props: {
    data: Object,
    active: Boolean
  },
  methods: {
    shake() {
      this.$nextTick(() => {
        this.$refs.appItem.classList.add('shake')
        setTimeout(() => {
          this.$refs.appItem.classList.remove('shake')
        }, 800)
      })
    }
  },
  watch: {
    active(n) {
      if (n) this.shake()
    }
  }
}
</script>

<style lang="scss" scoped>
.app-item {
  position: relative;
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 68px;
  height: 68px;
  border: 1px solid #ebeef5;
  margin-right: 16px;
  margin-bottom: 16px;
  border-radius: 8px;
  padding: 8px;
  box-sizing: border-box;
  cursor: pointer;

  &:hover {
    background: rgba(25, 137, 250, 0.2);
  }

  &-icon {
    display: flex;
    justify-content: center;
    font-size: 26px;
  }

  &-label {
    text-align: center;
    font-size: 12px;
    margin-top: 8px;
  }

  &-delete {
    position: absolute;
    right: -6px;
    top: -6px;
  }
}

.shake {
  animation: shake 800ms ease-in-out;
}
@keyframes shake {
  10%,
  90% {
    transform: translate3d(0, -1px, 0);
  }
  20%,
  80% {
    transform: translate3d(0, +2px, 0);
  }
  30%,
  70% {
    transform: translate3d(0, -4px, 0);
  }
  40%,
  60% {
    transform: translate3d(0, +4px, 0);
  }
  50% {
    transform: translate3d(0, -4px, 0);
  }
}
</style>
