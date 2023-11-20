<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2>Sửa thông báo</h2>
                        <div class="bg-secondary rounded p-4">
                            <form wire:submit="save">
                                <div class="form-floating mb-3">
                                    <h5 class="text-start">
                                        Thông báo hiển thị
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <textarea wire:model="content" class="form-control" style="height: 150px;"></textarea>
                                        <label>Miêu tả trò chơi</label>
                                    </div>
                                    @error('content')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
                                </div>
                                <hr />
                                <div class="py-2 text-start">
                                    <input wire:model="can_use" type="checkbox" class="form-check-input">
                                    Được sử dụng
                                </div>
                                <hr />
                                <button class="form-floating btn btn-success" type="submit"><i class="fa-regular fa-pen-to-square"></i> Sửa</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
