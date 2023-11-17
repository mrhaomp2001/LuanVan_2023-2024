<div>
    <div class="d-flex justify-content-center my-3">
        <div class="col-sm-12 col-xl-6">
            <div class="bg-secondary rounded h-100 p-4">
                <div class="d-inline-flex align-items-center">

                    <h6 class="mb-4 align-items-center">
                        <a wire:navigate href="{{ route('admin.user.index') }}" class="btn btn-square btn-primary m-2 align-items-center">
                            <i class="fa fa-arrow-left"></i>
                        </a>
                        Thông tin tài khoản
                    </h6>
                </div>
                <dl class="row mb-0">
                    <dt class="col-sm-4">Ảnh đại diện</dt>
                    <dd class="col-sm-8">
                        @if ($avatar_path != '')
                            <img id="output" class="img-thumbnail" style="max-height: 150px" src="{{ $avatar_path }}" />
                        @else
                            Chưa có ảnh đại diện
                        @endif
                    </dd>

                    <dt class="col-sm-4">Tên lớp học</dt>
                    <dd class="col-sm-8">{{ $name }}</dd>

                    <dt class="col-sm-4">Miêu tả</dt>
                    <dd class="col-sm-8">{{ $username }}</dd>


                    <dt class="col-sm-4 text-truncate">Trạng thái chặn</dt>
                    <dd class="col-sm-8">
                        @if ($is_ban)
                            <div class="text-danger">
                                Bị chặn
                            </div>
                        @else
                            <div class="text-success">
                                bình thường
                            </div>
                        @endif
                        </td>
                    </dd>
                    <hr />
                    <dt class="col-sm-4">Chỉnh sửa</dt>
                    <dd class="col-sm-8">
                        <button wire:click="save" wire:confirm="Bạn chắc chứ?" class="btn btn-outline-danger btn-sm">
                            @if ($is_ban)
                                <i class="fa-solid fa-lock-open me-2"></i>Bỏ chặn cho người này
                            @else
                                <i class="fa-solid fa-skull-crossbones me-2"></i>Chặn người này
                            @endif
                        </button>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
</div>
