<template>
  <div class="module-item">
    <div class="module-item-l">
      <div class="module-item-l-title">{{ data.name }}</div>
      <div class="module-item-l-desc">{{ data.description }}</div>
    </div>
    <div class="module-item-r"><em-icon :name="data.icon" /></div>

    <div class="module-item-version">v{{ data.version }}</div>
  </div>
</template>

<script>
export default {
  props: { data: Object }
}
</script>

<style lang="scss" scoped>
.module-item {
  position: relative;
  display: flex;
  width: calc(33.33% - 20px);
  height: 140px;
  box-shadow: 0 0 6px #ccc;
  border: 1px solid #e4e7ed;
  border-radius: 4px;
  margin-right: 20px;
  margin-bottom: 20px;
  padding: 26px 40px;
  box-sizing: border-box;
  cursor: pointer;

  transition: transform 0.2s;
  &:hover {
    transform: scale(1.02);
    border-top: 2px solid #4f8dfe;
    .module-item-r {
      transform: rotate(360deg);
    }
  }
  &-l {
    flex-grow: 1;
    box-sizing: border-box;

    &-title {
      width: 100px;
      height: 36px;
      line-height: 36px;
      text-align: center;
      font-size: 14px;
      background: #f6f6f9;
      border-radius: 4px;
      letter-spacing: 2px;
      font-weight: bold;
    }

    &-desc {
      margin-top: 12px;
      color: #888;
      line-height: 20px;

      &::before {
        content: "\25BA";
        margin-right: 4px;
      }
    }
  }
  &-r {
    flex-shrink: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 80px;
    height: 80px;
    border-radius: 50%;
    background: #f6f6f9;
    margin-left: 12px;
    margin-top: 4px;
    transition: transform 1s;

    .em-icon {
      font-size: 40px;
      font-weight: bold;
      color: #4f8dfe;
    }
  }
  &-version {
    position: absolute;
    right: 5px;
    top: 5px;

    width: 50px;
    height: 20px;
    line-height: 20px;
    text-align: center;
    background: #f56c6c;
    color: #fff;
    font-size: 10px;
    border-radius: 2px;
  }
}
</style>
