<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary rounded mb-3 p-3 d-inline-flex align-items-center">
            <div class="mx-2">
                <a wire:navigate href="{{ route('moderator.classrooms.show', ['id' => $classroom->id]) }}" class="btn btn-square btn-primary ">
                    <i class="fa fa-arrow-left "></i>
                </a>
            </div>
            <div>
                Bạn đang làm việc ở lớp <b>{{ $classroom->name }}</b>
            </div>
        </div>
        <div class="bg-secondary rounded p-4 ">
            <h2>Tạo trang tài liệu mới</h2>
            <form wire:submit="save">
                <div class="form-floating mb-3">
                    <h5>
                        Nội dung của tài liệu
                    </h5>
                    <div class="form-floating mb-3">
                        <textarea wire:model="content" class="form-control" placeholder="Nội dung của tài liệu học tập" name="content" id="content" style="height: 150px;"></textarea>
                        <label for="content">Nội dung của tài liệu</label>
                    </div>
                    @error('content')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <hr />
                <div class="mb-3 text-start">
                    <h5 for="image">Ảnh minh họa kiến thức</h5>
                    <input class="form-control form-control-sm bg-dark my-3" wire:model="image" type="file" accept=".png, .jpg" id="image" name="image">
                    <div class="row">
                        @isset($image)
                            <div class="col-6 col-ms-6">
                                <div class="" style="height: 50px">
                                    Ảnh minh họa kiến thức
                                </div>
                                <img id="output" class="img-thumbnail mx-2" style="max-height: 200px" src="{{ $image->temporaryUrl() }}" />
                            </div>
                        @endisset
                        @error('image')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                    </div>
                </div>
                <hr />
                <p>
                    Tài liệu sẽ được thêm vào trang cuối.
                </p>
                <button class="form-floating btn btn-success" type="submit">+ Thêm</button>
            </form>
        </div>
    </div>
</div>
