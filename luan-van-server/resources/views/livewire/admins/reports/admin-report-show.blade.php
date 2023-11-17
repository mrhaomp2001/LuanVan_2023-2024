<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary rounded p-3 d-inline-flex align-items-center">
            <div class="mx-2">
                <a wire:navigate href="{{ route('admin.report.index') }}" class="btn btn-square btn-primary ">
                    <i class="fa fa-arrow-left "></i>
                </a>
            </div>
            <div>
                Quay lại trang quản lý báo cáo
            </div>
        </div>
        <div class="d-flex justify-content-start my-3">
            <div class="col-sm-12 col-xl-6">
                <div class="bg-secondary rounded h-100 p-4">
                    <h4 class="text-start mb-1 py-1">
                        Chi tiết báo cáo
                    </h4>
                    <dl class="row mb-0">

                        <dt class="col-sm-4">Người báo cáo</dt>
                        <dd class="col-sm-8">{{ $report->user->name }}</dd>

                        <dt class="col-sm-4">Nội dung báo cáo</dt>
                        <dd class="col-sm-8">{{ $report->content }}</dd>

                        <dt class="col-sm-4">Loại báo cáo</dt>
                        <dd class="col-sm-8">{{ $report->reportType->name }}</dd>

                        <dt class="col-sm-4">Loại nội dung</dt>
                        <dd class="col-sm-8">{{ $report->model_type }}</dd>

                    </dl>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid pt-1 px-4">
        <div class="bg-secondary rounded p-4 ">
            <div class="bg-secondary rounded mb-3">
                <h4 class="text-start mb-1 py-1">
                    Nội dung bị báo cáo vi phạm
                </h4>
                <dl class="row mb-0">
                    <dt class="col-sm-4">Người bị báo cáo: </dt>
                    <dd class="col-sm-8">
                        {{ $report->model->user->name }}
                    </dd>

                    @if ($report->model->title != '')
                        <dt class="col-sm-4">
                            Tiêu đề:
                        </dt>
                        <dd class="col-sm-8">
                            {{ $report->model->title }}
                        </dd>
                    @endif

                    <dt class="col-sm-4">
                        Nội dung người dùng đã đăng:
                    </dt>
                    <dd class="col-sm-8">
                        {{ $report->model->content }}
                    </dd>
                    <div>
                        @if ($report->model->image_path != '')
                            <dt class="col-sm-4">
                                Ảnh người dùng đã đăng:
                            </dt>
                            <dd class="col-sm-8">
                                <img src="{{ $report->model->image_path }}"class="img-fluid py-1 img-thumbnail" style="max-height: 250px;">
                            </dd>
                        @endif
                    </div>
                </dl>

            </div>
        </div>
    </div>


    <div class="container-fluid pt-3 px-4">
        <div class="bg-secondary rounded p-4 ">
            <div class="bg-secondary rounded mb-3">
                <h4 class="text-start mb-1 py-1">
                    Xử lý báo cáo
                </h4>
                <div class="py-2">
                    Chọn một cách xử lý
                </div>
                <select wire:model="reponse_id" class="form-select mb-3" name="selectOpenClass">

                    @foreach ($reponses as $response)
                        <option class="text-white" value="{{ $response->id }}">{{ $response->content }}</option>
                    @endforeach

                </select>
                <div class="py-2">
                    <input wire:model="is_ban" type="checkbox" class="form-check-input">
                    Chặn người dùng vì hành vi sai phạm
                </div>


            </div>
            <div class="text-danger my-3">
                {{ $message }}
            </div>
            <button wire:click="action" class="btn btn-outline-primary">Xử lý</button>
        </div>
    </div>
</div>
