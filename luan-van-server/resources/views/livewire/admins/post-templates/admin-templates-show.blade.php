<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2>Sửa mẫu bài đăng</h2>
                        <div class="bg-secondary rounded p-4">
                            <form wire:submit="save">
                                <div>
                                    <h5 class="my-2 text-start">
                                        Tên mẫu
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <input wire:model="name" type="text" class="form-control">
                                        <label for="name">Miêu tả trò chơi</label>
                                    </div>

                                    @error('name')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
                                </div>
                                <div class="form-floating mb-3">
                                    <h5 class="text-start">
                                        Miêu tả mẫu
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <textarea wire:model="content" class="form-control" style="height: 150px;"></textarea>
                                        <label for="content">Miêu tả mẫu</label>
                                    </div>
                                    @error('content')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
                                </div>
                                <div>
                                    <h5 class="text-start">
                                        Màu chủ đề của mẫu
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <input wire:model="theme_color" type="color" class="form-control bg-dark mt-2" id="theme_color" name="theme_color" placeholder="Màu chủ đề" value="#ffffff">
                                        <label for="theme_color py-2">Màu chủ đề của mẫu (chỉ với mục đích trang trí)</label>
                                    </div>
                                </div>
                                <div class="py-2 text-start">
                                    <input wire:model="is_require_title" type="checkbox" class="form-check-input">
                                    Tiêu đề là bắt buộc
                                </div>
                                <div class="py-2 text-start">
                                    <input wire:model="is_require_image" type="checkbox" class="form-check-input">
                                    Ảnh đăng là bắt buộc
                                </div>
                                <hr />
                                <div class="text-start">
                                    @if ($can_use)
                                        Trạng thái: <span class="text-success">Được dùng</span>
                                    @else
                                        Trạng thái: <span class="text-danger">Không được dùng</span>
                                    @endif
                                </div>
                                <div class="py-2 text-start">
                                    <input wire:model="can_use" type="checkbox" class="form-check-input">
                                    Người dùng được sử dụng
                                </div>
                                <button class="form-floating btn btn-success" type="submit"><i class="fa-regular fa-pen-to-square"></i> Sửa</button>
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
