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
            <h2>Sửa trang tài liệu</h2>
            <form wire:submit="save">
                <div class="form-floating mb-3">
                    <h5>
                        Nội dung của tài liệu
                    </h5>
                    <div class="form-floating mb-3">
                        <textarea wire:model="content" class="form-control" placeholder="Nội dung của tài liệu học tập" name="content" id="content" style="height: 150px;"></textarea>
                        {{-- <label for="content">Nội dung của tài liệu</label> --}}
                    </div>
                    @error('content')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <h5>
                    Nội dung này sẽ xuất hiện ở trang:
                </h5>
                <div class="form-floating mb-3">

                    <input wire:model="page" type="number" class="form-control" id="page" name="page" value="">
                    <label for="page">Trang</label>

                    @error('page')
                        <p class="text-start text-danger">
                            {{ $message }}
                        </p>
                    @enderror
                </div>
                <hr />
                <div class="mb-3 text-start">
                    <h5 for="image">Cập nhật ảnh minh họa kiến thức</h5>
                    <input class="form-control form-control-sm bg-dark my-3" wire:model="image" type="file" accept=".png, .jpg" id="image" name="image">
                    <div class="row">
                        @isset($image)
                            <div class="col-6 col-ms-6">
                                <div class="" style="height: 50px">
                                    Ảnh minh họa kiến thức mới:
                                </div>
                                <img id="output" class="img-thumbnail mx-2" style="max-height: 200px" src="{{ $image->temporaryUrl() }}" />
                            </div>
                        @endisset
                        @error('image')
                            <p class="text-start text-danger">
                                {{ $message }}
                            </p>
                        @enderror

                        @isset($image_path)
                            <div class="col-6 col-ms-6">
                                <div class="" style="height: 50px">
                                    Ảnh minh họa hiện tại:
                                </div>
                                <img class="img-thumbnail mx-2 " style="max-height: 200px" src="{{ $image_path }}" />
                            </div>
                        @endisset
                    </div>
                </div>
                <hr />
                <button class="form-floating btn btn-success" type="submit"><i class="fa-regular fa-pen-to-square"></i> Sửa</button>
            </form>
            <hr />
            <h5>
                Xóa trang này
            </h5>
            <button class="form-floating btn btn-danger" wire:click="delete" wire:confirm="Are you sure you want to delete this?"><i class="fa-regular fa-trash-can"></i> Xóa</button>
        </div>
    </div>
</div>
