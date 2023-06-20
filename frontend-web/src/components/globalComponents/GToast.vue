<template>
    <div class="m-toast m-toast-success" v-if="isShowToast">
        <div class="m-toast-body">
            <div class="m-toast-message">
                <div class="m-toast-title">
                    <div class="icon">
                        <i class="fas fa-check-circle m-toast-icon"></i>
                    </div>
                    <p>Thành công!</p>
                </div>
                <div class="m-body-text">{{ msgToast }}</div>
            </div>
            <div class="m-toast-action">
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "GToast",
    created() {
        setTimeout(() => { this.isShowToast = false; }, 3000);
    },
    props: ["msgToast"],
    data() {
        return {
            isShowToast: true,
        }
    },
    methods: {

    }
}
</script>
<style scoped>
.m-toast {
    z-index: 9999;
}

.m-toast {
    width: fit-content;
    max-width: 650px;
    min-width: 400px;
    display: flex;
    justify-content: space-between;
    padding: 16px;
    background-color: #fff;
    justify-items: center;
    position: absolute;
    top: 95%;
    left: 100%;
    transform: translate(calc(-100% - 10px), calc(-100% - 40px));
    box-shadow: 2px 0 2px #e3e3e3, -2px -2px #e3e3e3;
    border-radius: 4px;
}

.m-toast-body {
    display: flex;
    width: 100%;
    justify-content: space-between;
    line-height: 24px;
}

.m-toast-message {
    display: flex;
    line-height: 24px;
    padding-right: 24px;
}

.m-toast-title {
    font-weight: bold;
    display: flex;
    line-height: 24px;
    padding-right: 4px;
}

.m-toast-title i {
    font-size: 20px;
    margin-right: 8px;
}

.m-toast-action {
    line-height: 24px;
}

.m-toast-action a {
    color: black;
    font-weight: bold;
}

.m-toast-close {
    width: 16px;
    height: 16px;
    margin: auto;
    text-align: center;
    margin-left: 16px;
    ;
}

.m-toast-success .m-toast-title {
    color: var(--success-color);
}

.m-toast-warning .m-toast-title {
    color: var(--warning-color);
}

.m-toast-error .m-toast-title {
    color: var(--error-color);
}

.m-toast-info .m-toast-title {
    color: var(--info-color);
}
</style>