<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2>Sửa thông tin lớp học</h2>
                        <div class="bg-secondary rounded p-4">
                            <form wire:submit="save">
                                <div>
                                    <input type="hidden" name="classroom_id" value="">
                                </div>
                                <div class="form-floating mb-3">
                                    <input wire:model.blur="name" type="text" class="form-control" id="name" name="name" placeholder="Tên lớp" value="">
                                    <label for="name">Tên lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <textarea wire:model="description" class="form-control" placeholder="Miêu tả lớp học" name="description" id="description" style="height: 200px;"></textarea>
                                    <label for="description">Miêu tả lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <input wire:model="theme_color" type="color" class="form-control bg-dark" id="theme_color" name="theme_color" placeholder="Màu chủ đề" value="#ffffff">
                                    <label for="theme_color">Màu chủ đề của lớp học</label>
                                </div>
                                <div class="text-start">
                                    <label class="mb-1">Trạng thái của lớp học:
                                        @if ($is_open)
                                            <span class="text-success">
                                                Đang mở
                                            </span>
                                        @else
                                            <span class="text-danger">
                                                Đang đóng
                                            </span>
                                        @endif
                                    </label>
                                    <select wire:model="is_open" class="form-select mb-3" name="selectOpenClass">
                                        <option value="0" disabled selected>Chọn 1 giá trị (tùy chọn)</option>
                                        <option value="true">Mở cửa lớp học</option>
                                        <option value="false">Đóng cửa lớp học</option>
                                    </select>
                                </div>
                                <hr />
                                <div class="mb-3 text-start">
                                    <div for="image">Thay đổi ảnh đại diện của lớp</div>
                                    <input class="form-control form-control-sm bg-dark my-3" wire:model="image" type="file" accept=".png, .jpg" id="image" name="image">
                                    <div class="form-check form-check-inline">
                                        <input wire:model="delete_image" class="form-check-input mb-3" type="checkbox" id="checkbox-delete-image" value="true">
                                        <label class="form-check-label" for="checkbox-delete-image">Xóa ảnh đại diện lớp học (Ấn sửa để có hiệu lực)</label>
                                    </div>
                                    <div class="row">
                                        @isset($image)
                                            <div class="col-6 col-ms-6">
                                                <div class="" style="height: 50px">
                                                    Ảnh đại diện mới:
                                                </div>
                                                <img id="output" class="img-thumbnail mx-2" style="max-height: 200px" src="{{ $image->temporaryUrl() }}" />
                                            </div>
                                        @endisset
                                        @isset($image_path)
                                            <div class="col-6 col-ms-6">
                                                <div class="" style="height: 50px">
                                                    Ảnh đại diện hiện tại:
                                                </div>
                                                <img class="img-thumbnail mx-2 " style="max-height: 200px" src="{{ $image_path }}" />
                                            </div>
                                        @endisset
                                    </div>
                                </div>
                                <hr />

                                <button class="form-floating btn btn-success" type="submit"><i class="fa fa-pen me-2"></i>Sửa</button>
                            </form>

                        </div>

                        @if ($errors->any())
                            <div>
                                @foreach ($errors->all() as $error)
                                    <div class="text-start text-danger fs-5">- {{ $error }}</div>
                                @endforeach
                            </div>
                        @endif
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
