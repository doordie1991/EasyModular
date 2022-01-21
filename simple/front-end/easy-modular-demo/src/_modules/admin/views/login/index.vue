<template>
  <div class="em-login">
    <div class="em-login-content">
      <div class="em-system-title">
        <div class="em-system-logo">
          <img :src="config.logo" />
        </div>
        <div class="em-system-name">
          <div class="em-system-name-cn">权限管理系统</div>
          <div class="em-system-name-en">Authority Management System</div>
        </div>
      </div>
      <div class="em-login-img"></div>
      <div class="em-login-form">
        <div class="em-login-form-title">欢迎登录</div>
        <el-form ref="form" :model="form" :rules="rules">
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
                <em-icon name="lock" />
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
          <div class="em-login-form-tip">账号:admin 密码:123</div>
        </el-form>
        <div class="em-login-form-other">
          <div class="em-login-form-other-item" v-for="(item, index) in others" :key="index">
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
<style lang="scss">
.em-login {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  background-size: 100% 100% !important;
  background: url('../../assets/images/login-bg.png');
  background-position: top center;

  &-content {
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 920px;
    height: 420px;

    .em-system-title {
      position: absolute;
      top: -120px;
      left: -60px;
      width: 100%;

      display: flex;
      .em-system-logo {
        width: 60px;
        height: 60px;

        img {
          width: 100%;
        }
      }

      .em-system-name {
        display: flex;
        flex-direction: column;
        height: 60px;
        margin-left: 20px;
        color: #fff;
        &-cn {
          height: 40px;
          line-height: 40px;
          font-size: 24px;
          font-weight: 600;
          letter-spacing: 4px;
        }
        &-en {
          height: 20px;
          line-height: 20px;
          font-size: 16px;
        }
      }
    }

    .em-login-img {
      width: 400px;
      height: 400px;
      background-size: 100% 100% !important;
      background: url('../../assets/images/login.png');
      background-position: top center;
      margin-right: 110px;
    }

    .em-login-form {
      width: 400px;
      height: 400px;
      padding: 30px 50px 30px 50px;
      border-radius: 20px;
      text-align: center;
      background: #fff;
      box-shadow: 0px 0px 12px #fff;
      box-sizing: border-box;

      &-title {
        font-size: 24px;
        color: #3b3b3b;
        font-weight: 600;
        letter-spacing: 2px;
        margin-bottom: 30px;
      }

      &-tip {
        text-align: left;
        color: #ccced1;
        margin-left: 8px;
      }

      &-other {
        display: flex;
        flex-direction: row;
        justify-content: center;
        height: 45px;
        margin-top: 16px;

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
        font-size: 18px !important;
        vertical-align: -3px;
        color: #1dcd9e;
        font-weight: 600;
      }

      .el-input__prefix {
        margin-left: 8px;
      }

      .el-input__inner {
        border-radius: 20px;
        padding-left: 40px !important;
        background-color: #f2f2f2 !important;
        border: none !important;
      }

      .el-button--primary {
        background-color: #1dcd9e !important;
        border-color: #1dcd9e !important;
        font-size: 14px;
        letter-spacing: 8px;
        border-radius: 20px;
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
