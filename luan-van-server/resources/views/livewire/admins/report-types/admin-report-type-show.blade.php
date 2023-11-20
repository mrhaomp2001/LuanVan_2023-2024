<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2>Sửa lý do báo cáo</h2>
                        <div class="bg-secondary rounded p-4">
                            <div class="text-white text-start">
                                Loại báo cáo:
                                <span>
                                    @if ($model_type == 'post')
                                        Báo cáo bài viết
                                    @endif
                                    @if ($model_type == 'comment')
                                        Báo cáo bình luận
                                    @endif
                                    @if ($model_type == 'topic')
                                        Báo cáo bài thảo luận
                                    @endif
                                    @if ($model_type == 'topic_comment')
                                        Báo cáo bình luận của bài thảo luận
                                    @endif
                                </span>
                            </div>
                            <hr />
                            <form wire:submit="save">
                                <div>
                                    <h5 class="my-2 text-start">
                                        Lý do báo cáo
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <input wire:model="name" type="text" class="form-control" id="name" name="name" placeholder="Tên bộ câu hỏi" value="">
                                        <label for="name">Lý do báo cáo</label>
                                    </div>

                                    @error('name')
                                        <p class="text-start text-danger">
                                            {{ $message }}
                                        </p>
                                    @enderror
                                </div>
                                <div class="form-floating mb-3 text-start">
                                    <h5>
                                        Miêu tả lý do
                                    </h5>
                                    <div class="form-floating mb-3">
                                        <textarea wire:model="description" class="form-control" name="description" id="description" style="height: 150px;"></textarea>
                                        <label for="description">Miêu tả lý do</label>
                                    </div>
                                    @error('description')
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
