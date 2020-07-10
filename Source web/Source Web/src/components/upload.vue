<template>
    <div class="upload">
        <el-upload action="http://localhost:58444/api/files/Upload"
                   list-type="picture-card"
                   :on-success="uploadSuccess"
                   :before-upload="beforeUpload">
            <i slot="default" class="el-icon-plus"></i>
            <div slot="file" slot-scope="{file}">
                <img class="el-upload-list__item-thumbnail"
                     :src="file.url" alt="">
                <span class="el-upload-list__item-actions">
                    <span class="el-upload-list__item-preview"
                          @click="handlePictureCardPreview(file)">
                        <i class="el-icon-zoom-in"></i>
                    </span>
                    <span v-if="!disabled"
                          class="el-upload-list__item-delete"
                          @click="handleDownload(file)">
                        <i class="el-icon-download"></i>
                    </span>
                    <span v-if="!disabled"
                          class="el-upload-list__item-delete"
                          @click="handleRemove(file)">
                        <i class="el-icon-delete"></i>
                    </span>
                </span>
            </div>
        </el-upload>
        <el-dialog :visible.sync="dialogVisible">
            <img width="100%" :src="dialogImageUrl" alt="">
        </el-dialog>
    </div>
</template>

<script>
    export default {
        name: 'upload',
        data() {
            return {
                dialogImageUrl: '',
                dialogVisible: false,
                disabled: false
            };
        },
        methods: {
            uploadSuccess(res, file) {
                this.dialogImageUrl = file.response.path;
            },
            handleRemove(file) {
                console.log(file);
            },
            handlePictureCardPreview(file) {
                this.dialogVisible = true;
            },
            handleDownload(file) {
                console.log(file);
            },
            beforeUpload(file) {
                const isJPG = file.type === 'image/jpeg';
                const isLt2M = file.size / 1024 / 1024 < 2;

                if (!isJPG) {
                    this.$message.error('上传头像图片只能是 JPG 格式!');
                }
                if (!isLt2M) {
                    this.$message.error('上传头像图片大小不能超过 2MB!');
                }
                return isJPG && isLt2M;
            }
        }
    };
</script>

<style scoped>
</style>

