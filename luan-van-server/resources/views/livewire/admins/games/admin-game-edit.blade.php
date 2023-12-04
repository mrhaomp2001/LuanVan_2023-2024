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
                                    <h5 class="my-2">
                                        Tên trò chơi
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <input wire:model="name" type="text" class="form-control" id="name" name="name" placeholder="Tên bộ câu hỏi" value="">
                                        <label for="name">Tên trò chơi</label>
                                    </div>

                                    @error('name')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
                                </div>
                                <div class="form-floating mb-3">
                                    <h5>
                                        Miêu tả trò chơi
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <textarea wire:model="description" class="form-control" name="description" id="description" style="height: 150px;"></textarea>
                                        <label for="content">Miêu tả trò chơi</label>
                                    </div>
                                    @error('description')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
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
