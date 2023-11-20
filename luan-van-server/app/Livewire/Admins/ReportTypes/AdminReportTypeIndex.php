<?php

namespace App\Livewire\Admins\ReportTypes;

use App\Models\ReportType;
use Livewire\Component;
use Livewire\WithPagination;

class AdminReportTypeIndex extends Component
{
    use WithPagination;

    public function render()
    {
        return view(
            'livewire.admins.report-types.admin-report-type-index',
            [
                'posts' => ReportType::where("model_type", "post")->paginate(10),
                'comments' => ReportType::where("model_type", "comment")->paginate(10),
                'topics' => ReportType::where("model_type", "topic")->paginate(10),
                'topic_comments' => ReportType::where("model_type", "topic_comment")->paginate(10),
            ]
        );
    }
}
