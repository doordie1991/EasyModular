<template>
  <div class="em-login">
    <div class="em-login-panel">
      <div class="em-login-content">
        <!--头部-->
        <div class="em-login-content-header">
          <div class="em-login-content-header-logo">
            <img class="em-login-content-header-logo-img" :src="config.logo" />
          </div>
          <div class="em-login-content-header-title">{{ config.title }}</div>
        </div>
        <!--表单-->
        <el-form class="em-login-content-form" ref="form" :model="form" :rules="rules">
          <el-form-item prop="userCode">
            <el-input v-model="form.userCode" placeholder="用户名">
              <template v-slot:prefix>
                <em-icon name="user" />
              </template>
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input type="password" v-model="form.password" autocomplete="off" placeholder="密码">
              <template v-slot:prefix>
                <em-icon name="eye" />
              </template>
            </el-input>
          </el-form-item>
          <div class="verifycode">
            <div class="verifycode-input">
              <el-form-item prop="verifyCode.code">
                <el-input v-model="form.verifyCode.code" autocomplete="off" placeholder="验证码">
                  <template v-slot:prefix>
                    <em-icon name="safetycertificate"></em-icon>
                  </template>
                </el-input>
              </el-form-item>
            </div>
            <div class="verifycode-img">
              <img title="点击刷新" :src="verifyCodeUrl" @click="refreshVierifyCode" />
            </div>
          </div>
          <el-form-item style="text-align: right">
            <el-button :loading="loading" class="btn-login" type="primary" @click="onLogin">登录</el-button>
          </el-form-item>
          <div class="em-login-content-form-tip">账号:admin 密码:123</div>
        </el-form>
        <!--第三方-->
        <div class="em-login-content-other">
          <div class="em-login-content-other-item" v-for="(item, index) in others" :key="index">
            <img :src="item.img" />
          </div>
        </div>
      </div>
    </div>
    <div class="em-login-copyright">{{ config.copyright }}</div>
  </div>
</template>
<script>
import { mapState, mapGetters, mapMutations } from 'vuex'
const { login, getAuthInfo, getVerifyCode } = $api.admin.auth

export default {
  data() {
    return {
      verifyCodeUrl: '',
      form: {
        userCode: '',
        password: '',
        verifyCode: {
          id: '',
          code: ''
        }
      },
      others: [{ img: require('../../assets/images/wechat.png') }, { img: require('../../assets/images/qq.png') }, { img: require('../../assets/images/qywx.png') }],
      rules: {
        userCode: [
          {
            required: true,
            message: '请输入用户名',
            trigger: 'blur'
          }
        ],
        password: [
          {
            required: true,
            message: '请输入密码',
            trigger: 'blur'
          }
        ],
        'verifyCode.code': [
          {
            required: true,
            message: '请输入验证码',
            trigger: 'blur'
          }
        ]
      },
      loading: false
    }
  },
  computed: {
    ...mapState('app/system', { config: (s) => s.config })
  },
  methods: {
    /**
     * @description: 登录
     * @param {*}
     */
    onLogin() {
      this.$refs.form.validate(async (valid) => {
        try {
          if (!valid) return false

          this.loading = true
          const result = await login(this.form)
          // 初始化令牌
          this.$store.commit('app/token/init', result)
          // 跳转
          let redirect = this.$route.query.redirect
          if (!redirect || redirect === '') {
            redirect = '/'
          }

          this.$router.push({
            path: redirect
          })
        } catch (err) {
          console.log(err)
        } finally {
          this.loading = false
        }
      })
    },

    /**
     * @description: 刷新验证码
     * @param {*}
     */
    async refreshVierifyCode() {
      const data = await getVerifyCode()
      this.verifyCodeUrl = data.base64String
      this.form.verifyCode.id = data.id
    }
  },
  mounted() {
    const _this = this
    document.onkeydown = function (e) {
      let ev = document.all ? window.event : e
      if (ev.keyCode === 13) {
        _this.onLogin()
      }
    }
    this.refreshVierifyCode()
  }
}
</script>
<style lang="scss" scoped>
.em-login {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;

  background-size: 100% 100% !important;
  background: url('../../assets/images/login-bg.png');
  background-position: top center;

  &-panel {
    width: 400px;
    height: 440px;
    padding: 10px 24px;
    border-radius: 5px;
    text-align: center;
    background: #fff;
    box-shadow: 0px 0px 12px #fff;
    margin-left: 62%;
    box-sizing:border-box;
  }

  &-content {
    &-header {
      width: 100%;
      height: 50px;
      line-height: 50px;
      display: flex;
      justify-content: center;
      margin-top: 30px;

      &-logo {
        text-align: center;

        &-img {
          height: 36px;
          vertical-align: middle;
          margin-right: 8px;
        }
      }

      &-title {
        text-align: center;
        font-size: 32px;
        font-weight: 800;
        letter-spacing: 2px;
        background-image: -webkit-linear-gradient(bottom, #1ccef4, #f053aa, #fbfcfd);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
      }
    }

    &-form {
      box-sizing: border-box;
      padding: 20px 30px 20px 30px;

      &-tip {
        text-align: left;
        color: #606266;
        font-size: 13px;
      }

      .verifycode {
        display: flex;

        &-input {
          padding-right: 10px;
          flex-grow: 1;
        }

        &-img {
          flex-shrink: 0;

          img {
            margin-top: 1px;
            height: 38px;
            cursor: pointer;
          }
        }
      }

      .btn-login {
        width: 100%;
        cursor: pointer;
      }

      .em-icon {
        font-size: 1.4em;
        vertical-align: -0.2em;
      }

      .el-input__inner {
        padding-left: 35px !important;
      }

      .el-button--primary {
        background-color: #6d95f1 !important;
        border-color: #6d95f1 !important;
        font-size: 14px;
        letter-spacing: 8px;
      }
    }

    &-other {
      display: flex;
      flex-direction: row;
      justify-content: center;
      height: 45px;

      &-item {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 40px;
        height: 40px;
        border-radius: 50%;
        margin-right: 10px;
        border: 1px solid #eff2f7;
        background: rgba(109, 149, 241, 0.03);
        transition: all 0.3s;
        img {
          width: 50%;
          height: 50%;
        }
        &:hover {
          cursor: pointer;
          background: rgba(109, 149, 241, 0.1);
          transform: scale(1.2);
        }
      }
    }
  }

  &-copyright {
    position: absolute;
    bottom: 12px;
    width: 100%;
    text-align: center;
    color: #e4e7ed;
  }
}
</style>
