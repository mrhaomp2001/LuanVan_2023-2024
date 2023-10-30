<?php

namespace App\Livewire\Admins\Reports;

use App\Models\Report;
use Livewire\Component;
use Livewire\WithPagination;

class AdminReportIndex extends Component
{
    use WithPagination;

    public function mount()
    {

    }

    public function render()
    {
        return view('livewire.admins.reports.admin-report-index', [
            'posts' => Report::where("model_type", "post")->paginate(7, pageName: 'posts-page'),
            'comments' =>  Report::where("model_type", "comment")->paginate(7, pageName: 'comments-page'),
            'topics' =>  Report::where("model_type", "topic")->paginate(7, pageName: 'topics-page'),
            'topic_comments' =>  Report::where("model_type", "topic_comment")->paginate(7, pageName: 'topic-comments-page'),
        ]);
    }
}
